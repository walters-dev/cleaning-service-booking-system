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
        string connectionString =
           "Server=localhost;Database=CleaningServiceBooking;Trusted_Connection=True;TrustServerCertificate=True;";//use method instead when it is made
        public IList<Bookings> GetBookings()
        {
            List<Bookings> bookingsInfo = new List<Bookings>();
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand command = new SqlCommand("",connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var booking = new Bookings();
                    {
                       // BookingId 
                    }
                }
            }
                return bookingsInfo;
        }
        public Bookings GetBookingsById(int? Id)
        {
            Bookings bookingsInfo = new Bookings();
            return bookingsInfo;
        }
        public void Add(Bookings bookings)
        {

        }
        public void Update(Bookings bookings)
        {

        }
        public void Delete(Bookings bookings)
        {

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
