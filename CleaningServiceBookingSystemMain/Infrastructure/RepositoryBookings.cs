using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryBookings : IBookingsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<Bookings> GetBookings()
        {
            List<Bookings> bookingsInfo = new List<Bookings>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetAllBookings", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    int active = reader.GetOrdinal("IsRecurring");
                    
                    var booking = new Bookings()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        CustomerId = reader.GetString(reader.GetOrdinal("Customers_id")),
                        HouseTypeId = reader.GetString(reader.GetOrdinal("Housetypes_id")),
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypes_id")),
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
                        FirstTimeBooking = reader.GetBoolean(reader.GetOrdinal("FirstTimeBooking")),
                        CarpetedRooms = reader.GetInt32(reader.GetOrdinal("CarpetedRooms")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                        UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    };
                    if (reader["DiscountRule_id"] != DBNull.Value)//checks if DiscountRule_id is null, if not then read the record
                    {
                        booking.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRule_id"));
                    }
                    else
                    {
                        booking.DiscountRuleId = null;
                    }
                    bookingsInfo.Add(booking);
                }
            }
                return bookingsInfo;
        }
        public void Add(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.AddBooking", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.Parameters.AddWithValue("@Customers_id", bookings.CustomerId);
                command.Parameters.AddWithValue("@Housetypes_id", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypes_id", bookings.ServiceTypeId);
                if (bookings.DiscountRuleId != null)
                {
                    command.Parameters.AddWithValue("@DiscountRule_id", bookings.DiscountRuleId);
                }
                else
                {
                    command.Parameters.AddWithValue("@DiscountRule_id", null);
                }
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
                command.Parameters.AddWithValue("@FirstTimeBooking", bookings.FirstTimeBooking);
                command.Parameters.AddWithValue("@CarpetedRooms", bookings.CarpetedRooms);
                command.Parameters.AddWithValue("@UpdatedAt", bookings.UpdatedAt);
                command.Parameters.AddWithValue("@UpdatedBy", bookings.UpdatedBy);
                command.ExecuteNonQuery();//executes the query and saves new booking to database
            }
        }
        public void Update(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.UpdateBooking", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.Parameters.AddWithValue("@Customers_id", bookings.CustomerId);
                command.Parameters.AddWithValue("@Housetypes_id", bookings.HouseTypeId);
                command.Parameters.AddWithValue("@ServiceTypes_id", bookings.ServiceTypeId);
                command.Parameters.AddWithValue("@DiscountRule_id", bookings.DiscountRuleId);
                command.Parameters.AddWithValue("@BookingDate", bookings.BookingDate);
                command.Parameters.AddWithValue("@NumberOfRooms", bookings.NumberOfRooms);
                command.Parameters.AddWithValue("@IsRecurring", bookings.IsRecurring);
                command.Parameters.AddWithValue("@RecurringBookingType", bookings.RecurringBookingType);
                command.Parameters.AddWithValue("@SubTotal", bookings.SubTotal);
                command.Parameters.AddWithValue("@DiscountAmount", bookings.DiscountAmount);
                command.Parameters.AddWithValue("@SurchargeAmount", bookings.SurchargeAmount);
                command.Parameters.AddWithValue("@TotalAmount", bookings.TotalAmount);
                command.Parameters.AddWithValue("@BookingStatus", bookings.BookingStatus);
                command.Parameters.AddWithValue("@FirstTimeBooking", bookings.FirstTimeBooking);
                command.Parameters.AddWithValue("@CarpetedRooms", bookings.CarpetedRooms);
                command.Parameters.AddWithValue("@UpdatedAt", bookings.UpdatedAt);
                command.Parameters.AddWithValue("@UpdatedBy", bookings.UpdatedBy);
                command.ExecuteNonQuery();//executes the query which saves changes to a pre-existing booking to the database
            }
        }
        public IList<BookingByDate> ListByRange(DateTime startDate, DateTime endDate)//sets up connection to database
        {
            List<BookingByDate> bookingsInfo = new List<BookingByDate>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.BookingListByDateRange", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var booking = new BookingByDate()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("CustomerName")),
                        HouseName = reader.GetString(reader.GetOrdinal("HouseName")),
                        ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<CustomerBookingHistory> BookingHistory(string phonenumber)
        {
            List<CustomerBookingHistory> bookingsInfo = new List<CustomerBookingHistory>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.CustomerBookingHistory", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var booking = new CustomerBookingHistory()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("CustomerName")),
                        HouseName = reader.GetString(reader.GetOrdinal("HouseName")),
                        ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                        NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                        BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }
        public IList<BookingRevenueSummary> RevenueSummary()
        {
            List<BookingRevenueSummary> bookingsInfo = new List<BookingRevenueSummary>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.RevenueSummary", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var booking = new BookingRevenueSummary()
                    {
                        ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
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
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.BookingsByHouseType", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var booking = new BookingByHouseType()
                    {
                        HouseName = reader.GetString(reader.GetOrdinal("HouseName")),
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
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.DiscountUsageSummary", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var booking = new BookingDiscountUsage()
                    {
                        DiscountName = reader.GetString(reader.GetOrdinal("DiscountName")),
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        Fullname = reader.GetString(reader.GetOrdinal("CustomerName")),
                        SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                        DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                        AmountAfterDiscount = reader.GetDecimal(reader.GetOrdinal("AmountAfterDiscount"))
                    };

                    bookingsInfo.Add(booking);
                }
            }
            return bookingsInfo;
        }

        public void ChangeBoookingStatus(Bookings bookings)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.ChangeBookingStatus", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", bookings.BookingId);
                command.Parameters.AddWithValue("@BookingStatus", bookings.BookingStatus);
                command.Parameters.AddWithValue("@UpdatedBy", bookings.UpdatedBy);
                command.Parameters.AddWithValue("@UpdatedAt", bookings.UpdatedAt);
                command.ExecuteNonQuery();//executes the query which saves booking status changes to a pre-existing booking to the database
            }
        }
        public string BookingsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.BookingsRowCount", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                if (reader.Read())
                {
                    id = reader.GetInt32("RowsCount");
                }
                else
                {
                    id = 0;
                }
            }
            return "BT" + (id + 1);
        }
        public IList<Bookings> GetBookingsCreatedToday()
        {
            List<Bookings> bookingsInfo = new List<Bookings>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetBookingsByCreatedDate", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var bookings = new Bookings()
                    {
                        BookingId = reader.GetString(reader.GetOrdinal("BookingId")),
                        CustomerId = reader.GetString(reader.GetOrdinal("Customers_id")),
                        HouseTypeId = reader.GetString(reader.GetOrdinal("Housetypes_id")),
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypes_id")),
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
                        FirstTimeBooking = reader.GetBoolean(reader.GetOrdinal("FirstTimeBooking")),
                        CarpetedRooms = reader.GetInt32(reader.GetOrdinal("CarpetedRooms")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                        UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    };
                    if (reader["DiscountRule_id"] != DBNull.Value)//checks if DiscountRule_id is null, if not then read the record
                    {
                        bookings.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRule_id"));
                    }
                    else
                    {
                        bookings.DiscountRuleId = null;
                    }
                    bookingsInfo.Add(bookings);
                }
                return bookingsInfo;
            }
        }
        public Bookings GetBookingsByPhoneNumberAndDate(string phonenumber, DateTime date)
        {
            Bookings booking = new Bookings();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetBookingsByPhoneNumberAndDate", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                command.Parameters.AddWithValue("@BookingDate", date);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    booking.BookingId = reader.GetString(reader.GetOrdinal("BookingId"));
                    booking.CustomerId = reader.GetString(reader.GetOrdinal("Customers_id"));
                    booking.HouseTypeId = reader.GetString(reader.GetOrdinal("Housetypes_id"));
                    booking.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypes_id"));
                    if (reader["DiscountRule_id"] != DBNull.Value)//checks if DiscountRule_id is null, if not then read the record
                    {
                        booking.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRule_id"));
                    }
                    else
                    {
                        booking.DiscountRuleId = null;
                    }
                    booking.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
                    booking.NumberOfRooms = reader.GetInt32(reader.GetOrdinal("NumberOfRooms"));
                    booking.IsRecurring = reader.GetBoolean(reader.GetOrdinal("IsRecurring"));
                    booking.RecurringBookingType = reader.GetString(reader.GetOrdinal("RecurringBookingType"));
                    booking.SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal"));
                    booking.DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount"));
                    booking.SurchargeAmount = reader.GetDecimal(reader.GetOrdinal("SurchargeAmount"));
                    booking.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                    booking.BookingStatus = reader.GetString(reader.GetOrdinal("BookingStatus"));
                    booking.UpdatedBy = reader.GetString(reader.GetOrdinal("UpdatedBy"));
                    booking.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));
                    booking.FirstTimeBooking = reader.GetBoolean(reader.GetOrdinal("FirstTimeBooking"));
                    booking.CarpetedRooms = reader.GetInt32(reader.GetOrdinal("CarpetedRooms"));
                }
                return booking;
            }
        }
    }
}
