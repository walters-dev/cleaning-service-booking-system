using CleaningServiceBookingSystemMain.Application;
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
        public IList<Admins> GetAdmins()
        {
            List<Admins> adminsInfo = new List<Admins>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var admins = new Admins()
                    {
                        AdminId = reader.GetString(reader.GetOrdinal("AddOnId")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        AdminPassword = reader.GetString(reader.GetOrdinal("AdminPassword")),
                        Email = reader.GetString(reader.GetOrdinal("Email"))
                    };
                    adminsInfo.Add(admins);
                }
            }
            return adminsInfo;
        }
        public Admins GetAdminsById(int? Id)
        {
            Admins adminsInfo = new Admins();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    adminsInfo.AdminId = reader.GetString(reader.GetOrdinal("AdminId"));
                    adminsInfo.Username = reader.GetString(reader.GetOrdinal("Username"));
                    adminsInfo.AdminPassword = reader.GetString(reader.GetOrdinal("AdminPassword"));
                    adminsInfo.Email = reader.GetString(reader.GetOrdinal("Email"));
                }
            }
            return adminsInfo;
        }
        public void Add(Admins admins)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBooking", connection);//waiting for sql procedure.......................................................................
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
        public void Update(Admins admins)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBooking", connection);//waiting for sql procedure.......................................................................
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
        public void Delete(Admins admins)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("DeleteCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@BookingId", admins.AdminId);
                command.ExecuteNonQuery();
            }
        }
    }
}
