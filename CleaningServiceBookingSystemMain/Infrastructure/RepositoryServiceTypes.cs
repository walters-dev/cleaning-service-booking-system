using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryServiceTypes : IServiceTypesRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<ServiceTypes> GetServiceTypes()
        {
            List<ServiceTypes> serviceTypesInfo = new List<ServiceTypes>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetAllServiceTypes", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var serviceTypes = new ServiceTypes()
                    {
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId")),
                        ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                        Multiplier = reader.GetDecimal(reader.GetOrdinal("Multiplier")),
                        ServiceDescription = reader.GetString(reader.GetOrdinal("ServiceDescription"))
                    };
                    serviceTypesInfo.Add(serviceTypes);
                }
            }
            return serviceTypesInfo;
        }
        public ServiceTypes GetServiceTypesById(string? Id)
        {
            ServiceTypes houseTypesInfo = new ServiceTypes();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetServiceType", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@ServiceId", Id);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    houseTypesInfo.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId"));
                    houseTypesInfo.ServiceName = reader.GetString(reader.GetOrdinal("ServiceName"));
                    houseTypesInfo.Multiplier = reader.GetDecimal(reader.GetOrdinal("Multiplier"));
                    houseTypesInfo.ServiceDescription = reader.GetString(reader.GetOrdinal("ServiceDescription"));
                }
            }
            return houseTypesInfo;
        }
    }
}
