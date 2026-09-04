using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class GetAdminPassword
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public string Password { get; private set; }
        public GetAdminPassword(string userName)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetAdminPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@Username", userName);
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                Password = reader.GetString(reader.GetOrdinal("Admin_Password"));
            }
        }
    }
}
