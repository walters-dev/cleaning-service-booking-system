using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Data;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class PrimaryKeyCreation
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
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

        public string CustomersRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.CustomersRowCount", connection);
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
            return "CT" + (id + 1);
        }
        public string HousetypesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.HousetypesRowCount", connection);
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
            return "HT" + (id + 1);
        }
        public string ServicetypesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.ServicetypesRowCount", connection);
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
            return "ST" + (id + 1);
        }

        public string DiscountRulesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.DiscountRulesRowCount", connection);
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
            return "DR" + (id + 1);
        }

        public string AddOnsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.DiscountRulesRowCount", connection);
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
            return "AD" + (id + 1);
        }

        public string BookingsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.DiscountRulesRowCount", connection);
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
            return "BT" + (id + 1);
        }

        public string BookingAddOnsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.DiscountRulesRowCount", connection);
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
            return "BA" + (id + 1);
        }
    }
}
