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
