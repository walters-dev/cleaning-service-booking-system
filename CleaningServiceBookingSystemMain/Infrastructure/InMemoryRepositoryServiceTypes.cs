using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryServiceTypes : IServiceTypesRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<ServiceTypes> GetServiceTypes()
        {
            List<ServiceTypes> serviceTypesInfo = new List<ServiceTypes>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var serviceTypes = new ServiceTypes()
                    {
                        ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId")),
                        ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                        Multiplier = reader.GetDecimal(reader.GetOrdinal("Multiplier")),
                        ServiceDescription = reader.GetString(reader.GetOrdinal("ServiceDescription")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                    serviceTypesInfo.Add(serviceTypes);
                }
            }
            return serviceTypesInfo;
        }
        public ServiceTypes GetServiceTypesById(string? Id)
        {
            ServiceTypes houseTypesInfo = new ServiceTypes();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    houseTypesInfo.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceTypeId"));
                    houseTypesInfo.ServiceTypeId = reader.GetString(reader.GetOrdinal("ServiceName"));
                    houseTypesInfo.Multiplier = reader.GetDecimal(reader.GetOrdinal("Multiplier"));
                    houseTypesInfo.ServiceDescription = reader.GetString(reader.GetOrdinal("ServiceDescription"));
                    houseTypesInfo.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                }
            }
            return houseTypesInfo;
        }
    }
}
