using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Intrinsics.Arm;
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
                    int active = reader.GetOrdinal("IsRecurring");
                    var booking = new Bookings()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        CustomerId = reader.GetString(reader.GetOrdinal("Customers_id")),
                        HouseTypeId = reader.GetString(reader.GetOrdinal("Housetypes_id")),
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypes_id")),
                        DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRule_id")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms")),
                        //IsRecurring = reader.IsDBNull(active) ? (bool?)null : reader.GetBoolean(active);
                        //IsRecurring = reader.GetBoolean(reader.GetOrdinal("IsRecurring")),//............................................
                        RecurringBookingType = reader.GetString(reader.GetOrdinal("RecurringBookingType")),
                        SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                        DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                        SurchargeAmount = reader.GetDecimal(reader.GetOrdinal("SurchargeAmount")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"))
                    };
                    bookingsInfo.Add(booking);
                }
            }
                return bookingsInfo;
        }
        public Bookings GetBookingsById(string? Id)
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
                    bookingsInfo.CustomerId = reader.GetString(reader.GetOrdinal("Customers_id"));
                    bookingsInfo.HouseTypeId = reader.GetString(reader.GetOrdinal("Housetypes_id"));
                    bookingsInfo.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypes_id"));
                    bookingsInfo.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRule_id"));
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
                command.Parameters.AddWithValue("@Customers_id", bookings.CustomerId);
                command.Parameters.AddWithValue("@Housetypes_id", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypes_id", bookings.ServiceTypeId);
                command.Parameters.AddWithValue("@DiscountRule_id", bookings.DiscountRuleId);
                command.Parameters.AddWithValue("@BookingDate", bookings.BookingDate);
                command.Parameters.AddWithValue("@NumberOfRooms", bookings.NumberOfRooms);
                command.Parameters.AddWithValue("@IsRecurring", bookings.IsRecurring);//............................................ 
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
                command.Parameters.AddWithValue("@Customers_id", bookings.CustomerId);
                command.Parameters.AddWithValue("@Housetypes_id", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypes_id", bookings.ServiceTypeId);
                command.Parameters.AddWithValue("@DiscountRule_id", bookings.DiscountRuleId);
                command.Parameters.AddWithValue("@BookingDate", bookings.BookingDate);
                command.Parameters.AddWithValue("@NumberOfRooms", bookings.NumberOfRooms);
                command.Parameters.AddWithValue("@IsRecurring", bookings.IsRecurring);//............................................
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
        public IList<BookingByDate> ListByRange(DateTime startDate, DateTime endDate)
        {
            List<BookingByDate> bookingsInfo = new List<BookingByDate>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("BookingListByDateRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new BookingByDate()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("b.BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("c.Fullname")),
                        HouseName = reader.GetString(reader.GetOrdinal("h.HouseName")),
                        ServiceName = reader.GetString(reader.GetOrdinal("s.ServiceName")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("b.BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("b.NumberOfRooms")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("b.TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("b.BookingStatus"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<CustomerBookingHistory> BookingHistory()
        {
            List<CustomerBookingHistory> bookingsInfo = new List<CustomerBookingHistory>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("BookingListByDateRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                //command.Parameters.AddWithValue("@StartDate", startDate);
                //command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new CustomerBookingHistory()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("b.BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("c.Fullname")),
                        HouseName = reader.GetString(reader.GetOrdinal("h.HouseName")),
                        ServiceName = reader.GetString(reader.GetOrdinal("s.ServiceName")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("b.BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("b.NumberOfRooms")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("b.TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("b.BookingStatus"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<BookingRevenueSummary> RevenueSummary()
        {
            List<BookingRevenueSummary> bookingsInfo = new List<BookingRevenueSummary>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("BookingListByDateRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                //command.Parameters.AddWithValue("@StartDate", startDate);
                //command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new BookingRevenueSummary()
                    {
                        ServiceName = reader.GetString(reader.GetOrdinal("s.ServiceName")),
                        BookingCount = reader.GetInt32(reader.GetOrdinal("BookingCount")),
                        TotalRevenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<BookingByHouseType> BookingsByHouseType()
        {
            List<BookingByHouseType> bookingsInfo = new List<BookingByHouseType>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("BookingListByDateRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                //command.Parameters.AddWithValue("@StartDate", startDate);
                //command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new BookingByHouseType()
                    {
                        HouseName = reader.GetString(reader.GetOrdinal("h.HouseName")),
                        BookingCount = reader.GetInt32(reader.GetOrdinal("BookingCount"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<BookingDiscountUsage> DiscountUsage()
        {
            List<BookingDiscountUsage> bookingsInfo = new List<BookingDiscountUsage>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("BookingListByDateRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                //command.Parameters.AddWithValue("@StartDate", startDate);
                //command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new BookingDiscountUsage()
                    {
                        DiscountName = reader.GetString(reader.GetOrdinal("d.DiscountName")),
                        BookingId = reader.GetString(reader.GetOrdinal("b.BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("c.Fullname")),
                        SubTotal = reader.GetDecimal(reader.GetOrdinal("b.SubTotal")),
                        DiscountAmount = reader.GetDecimal(reader.GetOrdinal("b.DiscountAmount")),
                        AmountAfterDiscount = reader.GetDecimal(reader.GetOrdinal("AmountAfterDiscount"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
    }
}
