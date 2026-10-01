using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CleaningServiceBookingSystemMain.Application.Interfaces;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryAddOns : IAddOnsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<AddOns> GetAddOns()
        {
            List<AddOns> addOnsInfo = new List<AddOns>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetAllAddOns", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var addOns = new AddOns()
                    {
                        AddOnId = reader.GetString(reader.GetOrdinal("AddOnId")),
                        AddOnsName = reader.GetString(reader.GetOrdinal("AddOnsName")),
                        Rate = reader.GetDecimal(reader.GetOrdinal("Rate")),
                        PricingType = reader.GetString(reader.GetOrdinal("PricingType"))
                    };
                    addOnsInfo.Add(addOns);
                }
            }
            return addOnsInfo;
        }
        public AddOns GetAddOnByAddOnId(string Id)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                AddOns addOn = new AddOns();
                SqlCommand command = new SqlCommand("dbo.GetAddOnByAddOnId", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@AddOnId", Id);
                using SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                if (reader.Read())
                {
                    addOn.AddOnId = reader.GetString(reader.GetOrdinal("AddOnId"));
                    addOn.AddOnsName = reader.GetString(reader.GetOrdinal("AddOnsName"));
                    addOn.Rate = reader.GetDecimal(reader.GetOrdinal("Rate"));
                    addOn.PricingType = reader.GetString(reader.GetOrdinal("PricingType"));
                    return addOn;
                }
                else
                {
                    return addOn;
                }
            }
        }
    }
}
