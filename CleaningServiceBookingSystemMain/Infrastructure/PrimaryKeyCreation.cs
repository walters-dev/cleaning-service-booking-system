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
                SqlCommand command = new SqlCommand("AdminRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "AT" + (id + 1);
        }

        public string CustomersRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("CustomersRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "CT" + (id + 1);
        }
        public string HousetypesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("HousetypesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "HT" + (id + 1);
        }
        public string ServicetypesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("ServicetypesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "ST" + (id + 1);
        }

        public string DiscountRulesRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DiscountRulesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "DR" + (id + 1);
        }

        public string AddOnsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DiscountRulesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "AD" + (id + 1);
        }

        public string BookingsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DiscountRulesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "BT" + (id + 1);
        }

        public string BookingAddOnsRowCount()
        {
            int id;
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DiscountRulesRowCount", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                id = reader.GetInt32("RowsCount");
            }
            return "BA" + (id + 1);
        }
    }
}
