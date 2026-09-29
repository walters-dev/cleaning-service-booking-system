using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using CleaningServiceBookingSystemMain.Application.Interfaces;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryAddOns : IAddOnsRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<AddOns> GetAddOns()
        {
            List<AddOns> addOnsInfo = new List<AddOns>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetAllAddOns", connection);//gets stored procedure from database
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
                        PricingType = reader.GetString(reader.GetOrdinal("PricingType"))
                    };
                    addOnsInfo.Add(addOns);
                }
            }
            return addOnsInfo;
        }
        
    }
}
