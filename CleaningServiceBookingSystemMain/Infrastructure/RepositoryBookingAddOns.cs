using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryBookingAddOns : IBookingAddOnsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public void Add(BookingAddOns bookingAddOns)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.AddBookingAddOn", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@BookingAddOnId", bookingAddOns.BookingAddOnId);
                command.Parameters.AddWithValue("@Booking_id", bookingAddOns.BookingId);
                command.Parameters.AddWithValue("@AddOn_id", bookingAddOns.AddOnId);
                command.Parameters.AddWithValue("@Quantity", bookingAddOns.Quantity);
                command.Parameters.AddWithValue("@LineAmount", bookingAddOns.LineAmount);
                command.ExecuteNonQuery();//executes the query which saves a new booking add on to database
            }
        }
        public void DeleteBookingAddOnByBookingId(string Id)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.DeleteBookingAddOnByBookingId", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", Id);
                command.ExecuteNonQuery();//executes the query which deletes booking add ons with booking id in the database
            }
        }
        public int GetLastPrimaryKeyAddOnBookings()
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                char[] removeChars = { 'B', 'A' };//list of characters to remove
                SqlCommand command = new SqlCommand("dbo.GetLastRowAddOnBookings", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                int IdNumber = 0;
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    //removes letters and returns the integer left over 
                    int readNumber = Int32.Parse(reader.GetString(reader.GetOrdinal("BookingAddOnId")).Trim(removeChars));
                    if (IdNumber< readNumber)
                    {
                        IdNumber = readNumber;
                    }
                }
                return IdNumber;
            }
        }
        public IList<BookingAddOns> GetBookingAddOnsByBookingId(string Id)
        {
            List<BookingAddOns> bookingAddOns = new List<BookingAddOns>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetBookingAddOnsByBookingId", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@BookingId", Id);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var bookingAddOn = new BookingAddOns();
                    {
                        bookingAddOn.BookingAddOnId = reader.GetString(reader.GetOrdinal("BookingAddOnId"));
                        bookingAddOn.BookingId = reader.GetString(reader.GetOrdinal("Booking_id"));
                        bookingAddOn.AddOnId = reader.GetString(reader.GetOrdinal("AddOn_id"));
                        bookingAddOn.LineAmount = reader.GetDecimal(reader.GetOrdinal("LineAmount"));
                        bookingAddOn.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")); 
                    }
                    ;
                    bookingAddOns.Add(bookingAddOn);

                }
                
            }
            if (bookingAddOns.Count == 0)//if no bookings addons exist throw exception
            {
                throw new ArgumentException();
            }
            return bookingAddOns;

        }
    }
}
