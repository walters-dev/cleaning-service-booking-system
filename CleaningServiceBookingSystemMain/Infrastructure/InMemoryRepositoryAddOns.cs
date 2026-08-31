using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using CleaningServiceBookingSystemMain.Application;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryAddOns : IAddOnsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<AddOns> GetAddOns()
        {
            List<AddOns> addOnsInfo = new List<AddOns>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var addOns = new AddOns()
                    {
                        AddOnId = reader.GetString(reader.GetOrdinal("AddOnId")),
                        AddOnsName = reader.GetString(reader.GetOrdinal("AddOnsName")),
                        Rate = reader.GetDecimal(reader.GetOrdinal("Rate")),
                        PricingType = reader.GetString(reader.GetOrdinal("PricingType")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                    addOnsInfo.Add(addOns);
                }
            }
            return addOnsInfo;
        }
        public AddOns AddOnsByID(int? Id)
        {
            AddOns addOnsInfo = new AddOns();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    addOnsInfo.AddOnId = reader.GetString(reader.GetOrdinal("AddOnId"));
                    addOnsInfo.AddOnsName = reader.GetString(reader.GetOrdinal("AddOnsName"));
                    addOnsInfo.Rate = reader.GetDecimal(reader.GetOrdinal("Rate"));
                    addOnsInfo.PricingType = reader.GetString(reader.GetOrdinal("PricingType"));
                    addOnsInfo.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                }
            }
            return addOnsInfo;
        }
    }
}
