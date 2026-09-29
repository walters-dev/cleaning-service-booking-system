using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryBookingAddOns : IBookingAddOnsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<BookingAddOns> GetBookingAddOns()
        {
            List<BookingAddOns> bookingAddOnsInfo = new List<BookingAddOns>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var bookingAddOns = new BookingAddOns()
                    {
                        BookingAddOnId = reader.GetString(reader.GetOrdinal("BookingAddOnId")),
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        AddOnId = reader.GetString(reader.GetOrdinal("AddOnId")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                        LineAmount = reader.GetDecimal(reader.GetOrdinal("LineAmount"))
                    };
                    bookingAddOnsInfo.Add(bookingAddOns);
                }
                
            }
            return bookingAddOnsInfo;
        }
        public BookingAddOns bookingAddOnsByID(string? Id)
        {
            BookingAddOns bookingAddOnsInfo = new BookingAddOns();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookingAddOnsInfo.BookingAddOnId = reader.GetString(reader.GetOrdinal("BookingAddOnId"));
                    bookingAddOnsInfo.BookingId = reader.GetString(reader.GetOrdinal("BookingId"));
                    bookingAddOnsInfo.AddOnId = reader.GetString(reader.GetOrdinal("AddOnId"));
                    bookingAddOnsInfo.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                    bookingAddOnsInfo.LineAmount = reader.GetDecimal(reader.GetOrdinal("LineAmount"));
                }
            }
            return bookingAddOnsInfo;
        }
        public void Add(BookingAddOns bookingAddOns)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.AddBookingAddOn", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@BookingAddOnId", bookingAddOns.BookingAddOnId);
                command.Parameters.AddWithValue("@Booking_id", bookingAddOns.BookingId);
                command.Parameters.AddWithValue("@AddOn_id", bookingAddOns.AddOnId);
                command.Parameters.AddWithValue("@Quantity", bookingAddOns.Quantity);
                command.Parameters.AddWithValue("@LineAmount", bookingAddOns.LineAmount);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteBookingAddOnByBookingId(string Id)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.DeleteBookingAddOnByAddOnId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", Id);
                command.ExecuteNonQuery();
            }
        }
        public int GetLastRowAddOnBookings()
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                char[] removeChars = { 'B', 'A' };//list of characters to remove
                SqlCommand command = new SqlCommand("dbo.GetLastRowAddOnBookings", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    //removes letters and returns the integer left over 
                    return Int32.Parse(reader.GetString(reader.GetOrdinal("BookingAddOnId")).Trim(removeChars));
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
