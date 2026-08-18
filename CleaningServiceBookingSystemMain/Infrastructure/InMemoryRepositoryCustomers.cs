using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryCustomers : ICustomerRepository
    {
        string connectionString =
            "Server=localhost;Database=CleaningServiceBooking;Trusted_Connection=True;TrustServerCertificate=True;";
         public IList<Customers> GetCustomers()
        {
            List<Customers> customersInfo = new List<Customers>();
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand("queryString", connection);//waiting for sql procedure
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var customer = new Customers()
                    {
                        CustomerId = reader["CustomerId"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhyAddress = reader["Phy"].ToString(),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                    };
                }
                //command.ExecuteNonQuery();
            }
            return customersInfo;
        }
        public Customers GetCustomerById(int? Id)// ? means it can be null
        {
            Customers customers = new Customers();
            return customers;
        }
        public void Add(Customers customers)
        {

        }
        public void Update(Customers customers)
        {

        }
        public void Delete(Customers customers)
        {

        }
    }
}
