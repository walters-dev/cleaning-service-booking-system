using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryBookings : IBookingsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<Bookings> GetBookings()
        {
            List<Bookings> bookingsInfo = new List<Bookings>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj",connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new Bookings()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        CustomerId = reader.GetString(reader.GetOrdinal("CustomerId")),
                        HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypeId")),
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId")),
                        DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRuleId")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms")),
                        IsRecurring = reader.GetBoolean(reader.GetOrdinal("IsRecurring")),
                        RecurringBookingType = reader.GetString(reader.GetOrdinal("RecurringBookingType")),
                        SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                        DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                        SurchargeAmount = reader.GetDecimal(reader.GetOrdinal("SurchargeAmount")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                    };
                    bookingsInfo.Add(booking);
                }
            }
                return bookingsInfo;
        }
        public Bookings GetBookingsById(int? Id)
        {
            Bookings bookingsInfo = new Bookings();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetBooking", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookingsInfo.BookingId = reader.GetString(reader.GetOrdinal("BookingId"));
                    bookingsInfo.CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"));
                    bookingsInfo.HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypeId"));
                    bookingsInfo.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId"));
                    bookingsInfo.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRuleId"));
                    bookingsInfo.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
                    bookingsInfo.NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms"));
                    bookingsInfo.IsRecurring = reader.GetBoolean(reader.GetOrdinal("IsRecurring"));
                    bookingsInfo.RecurringBookingType = reader.GetString(reader.GetOrdinal("RecurringBookingType"));
                    bookingsInfo.SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal"));
                    bookingsInfo.DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount"));
                    bookingsInfo.SurchargeAmount = reader.GetDecimal(reader.GetOrdinal("SurchargeAmount"));
                    bookingsInfo.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                    bookingsInfo.BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"));
                    bookingsInfo.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    bookingsInfo.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
                    bookingsInfo.UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"));
                    bookingsInfo.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));
                }
            }
            return bookingsInfo;
        }
        public void Add(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBooking", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.Parameters.AddWithValue("@CustomerId", bookings.CustomerId);
                command.Parameters.AddWithValue("@HouseTypeId", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypeId", bookings.ServiceTypeId);
                command.Parameters.AddWithValue("@DiscountRuleId", bookings.DiscountRuleId);
                command.Parameters.AddWithValue("@BookingDate", bookings.BookingDate);
                command.Parameters.AddWithValue("@NumberOfRooms", bookings.NumberOfRooms);
                command.Parameters.AddWithValue("@IsRecurring", bookings.IsRecurring); 
                command.Parameters.AddWithValue("@RecurringBookingType", bookings.RecurringBookingType); 
                command.Parameters.AddWithValue("@SubTotal", bookings.SubTotal); 
                command.Parameters.AddWithValue("@DiscountAmount", bookings.DiscountAmount); 
                command.Parameters.AddWithValue("@SurchargeAmount", bookings.SurchargeAmount); 
                command.Parameters.AddWithValue("@TotalAmount", bookings.TotalAmount); 
                command.Parameters.AddWithValue("@BookingStatus", bookings.BookingStatus); 
                command.Parameters.AddWithValue("@CreatedAt", bookings.CreatedAt); 
                command.Parameters.AddWithValue("@CreatedBy", bookings.CreatedBy); 
                command.ExecuteNonQuery();
            }
        }
        public void Update(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("UpdateBooking", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.Parameters.AddWithValue("@CustomerId", bookings.CustomerId);
                command.Parameters.AddWithValue("@HouseTypeId", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypeId", bookings.ServiceTypeId);
                command.Parameters.AddWithValue("@DiscountRuleId", bookings.DiscountRuleId);
                command.Parameters.AddWithValue("@BookingDate", bookings.BookingDate);
                command.Parameters.AddWithValue("@NumberOfRooms", bookings.NumberOfRooms);
                command.Parameters.AddWithValue("@IsRecurring", bookings.IsRecurring);
                command.Parameters.AddWithValue("@RecurringBookingType", bookings.RecurringBookingType);
                command.Parameters.AddWithValue("@SubTotal", bookings.SubTotal);
                command.Parameters.AddWithValue("@DiscountAmount", bookings.DiscountAmount);
                command.Parameters.AddWithValue("@SurchargeAmount", bookings.SurchargeAmount);
                command.Parameters.AddWithValue("@TotalAmount", bookings.TotalAmount);
                command.Parameters.AddWithValue("@BookingStatus", bookings.BookingStatus);
                command.Parameters.AddWithValue("@UpdatedAt", bookings.UpdatedAt);
                command.Parameters.AddWithValue("@UpdatedBy", bookings.UpdatedBy);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DeleteCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.ExecuteNonQuery();
            }
        }
        public void DiaplayListByRange(Bookings bookings)
        {

        }
        public void DisplayBookingHistory(Bookings bookings)
        {

        }
        public void DisplayRevenueSummary(Bookings bookings)
        {

        }
        public void DisplayBookingsByHouseType(Bookings bookings)
        {

        }
        public void DisplayDiscountUsage(Bookings bookings)
        {

        }
    }
}
