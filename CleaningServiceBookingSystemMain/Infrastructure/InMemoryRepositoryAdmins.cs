using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryAdmins : IAdminRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();

        public void Add(Admins admins)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.AddBooking", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@AdminId", admins.AdminId);
                command.Parameters.AddWithValue("@Username", admins.Username);
                command.Parameters.AddWithValue("@AdminPassword", admins.AdminPassword);
                command.Parameters.AddWithValue("@Email", admins.Email);
                command.ExecuteNonQuery();
            }
        }

        public string GetAdminPasswordByUsername(string userName)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetAdminPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@Username", userName);
                command.ExecuteNonQuery();
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(reader.GetOrdinal("Admin_Password"));
                }
                else
                {
                    return null;
                }
            }
            
        }

        public string AdminRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.AdminRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id = reader.GetInt32("RowsCount");
                }
                else
                {
                    id = 0;
                }
            }
            return "AT" + (id + 1);
        }

    }
}
