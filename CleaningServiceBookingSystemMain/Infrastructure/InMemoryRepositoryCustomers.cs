using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryCustomers : ICustomerRepository
    {
        string connectionString =
            "Server=localhost;Database=CleaningServiceBooking;Trusted_Connection=True;TrustServerCertificate=True;";
        IList<Customers> GetCustomers()
        {
            List<Customers> customersInfo = new List<Customers>();
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

        }
        Customers GetCustomerById(int? Id)// ? means it can be null
        {

        }
        void Add(Customers customers);
        void Update(Customers customers);
        void Delete(Customers customers);
    }
}
