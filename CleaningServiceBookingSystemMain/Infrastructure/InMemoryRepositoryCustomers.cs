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
        //methods need to be public or cannot implement interface member
        DatabaseConnection databaseConnection = new DatabaseConnection();
         public IList<Customers> GetCustomers()
        {
            List<Customers> customersInfo = new List<Customers>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetAllCustomers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var customer = new Customers()
                    {
                        CustomerId = reader.GetString(reader.GetOrdinal("CustomerId")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PhyAddress = reader.GetString(reader.GetOrdinal("PhyAddress")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatdeBy = reader.GetString(reader.GetOrdinal("UpdatedBy")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                    };
                    customersInfo.Add(customer);
                }
                
            }
            return customersInfo;
        }
        public Customers GetCustomerById(int? Id)// ? means it can be null
        {
            Customers customersInfo = new Customers();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customersInfo.CustomerId = reader.GetString(reader.GetOrdinal("CustomerId"));
                    customersInfo.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    customersInfo.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                    customersInfo.Email = reader.GetString(reader.GetOrdinal("Email"));
                    customersInfo.PhyAddress = reader.GetString(reader.GetOrdinal("PhyAddress"));
                    customersInfo.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    customersInfo.CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy"));
                    customersInfo.UpdatdeBy = reader.GetString(reader.GetOrdinal("UpdatedBy"));
                    customersInfo.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"));
                }
            }
            return customersInfo;
        }
        public void Add(Customers customers)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@CustomerID", customers.CustomerId);
                command.Parameters.AddWithValue("@Fullname", customers.FullName);
                command.Parameters.AddWithValue("@PhoneNumber", customers.PhoneNumber);
                command.Parameters.AddWithValue("@Email", customers.Email);
                command.Parameters.AddWithValue("@PhyAddress", customers.PhyAddress);
                command.Parameters.AddWithValue("@CreatedAt", customers.CreatedAt);
                command.Parameters.AddWithValue("@CreatedBy", customers.CreatedAt);
                command.ExecuteNonQuery();
            }
        }
        public void Update(Customers customers)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("UpdateCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@Fullname", customers.FullName);
                command.Parameters.AddWithValue("@PhoneNumber", customers.PhoneNumber);
                command.Parameters.AddWithValue("@Email", customers.Email);
                command.Parameters.AddWithValue("@PhyAddress", customers.PhyAddress);
                command.Parameters.AddWithValue("@UpdatedAt", customers.UpdatedAt);
                command.Parameters.AddWithValue("@UpdatedBy", customers.UpdatdeBy);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(Customers customers)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DeleteCustomer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@CustomerID", customers.CustomerId);
                command.ExecuteNonQuery();
            }
        }
    }
}
