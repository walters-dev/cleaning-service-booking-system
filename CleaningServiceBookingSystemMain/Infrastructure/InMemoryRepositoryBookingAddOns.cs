using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryBookingAddOns : IBookingAddOnsRepository
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
                        LineAmount = reader.GetString(reader.GetOrdinal("LineAmount"))
                    };
                    bookingAddOnsInfo.Add(bookingAddOns);
                }
            }
            return bookingAddOnsInfo;
        }
        public BookingAddOns bookingAddOnsByID(int? Id)
        {
            BookingAddOns bookingAddOnsInfo = new BookingAddOns();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookingAddOnsInfo.BookingAddOnId = reader.GetString(reader.GetOrdinal("BookingAddOnId"));
                    bookingAddOnsInfo.BookingId = reader.GetString(reader.GetOrdinal("BookingId"));
                    bookingAddOnsInfo.AddOnId = reader.GetString(reader.GetOrdinal("AddOnId"));
                    bookingAddOnsInfo.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                    bookingAddOnsInfo.LineAmount = reader.GetString(reader.GetOrdinal("LineAmount"));
                }
            }
            return bookingAddOnsInfo;
        }
        public void Add(BookingAddOns bookingAddOns)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBookingAddOn", connection);
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
        public void Update(BookingAddOns bookingAddOns)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBooking", connection);//waiting for sql procedure.......................................................................
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
    }
}
