using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryHouseTypes : IHouseTypesRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<HouseTypes> GetHouseTypes()
        {
            List<HouseTypes> houseTypesInfo = new List<HouseTypes>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetAllHouseTypes", connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var houseTypes = new HouseTypes()
                    {
                        HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypeId")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        BaseRate = reader.GetDecimal(reader.GetOrdinal("BaseRate")),
                        RatePerRate = reader.GetDecimal(reader.GetOrdinal("RatePerRate")),
                        MinRooms = reader.GetInt32(reader.GetOrdinal("MinRooms")),
                        MaxRooms = reader.GetInt32(reader.GetOrdinal("MaxRooms")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                    houseTypesInfo.Add(houseTypes);
                }
            }
            return houseTypesInfo;
        }
        public HouseTypes GetHouseTypesById(int? Id)
        {
            HouseTypes houseTypesInfo = new HouseTypes();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    houseTypesInfo.HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypeId"));
                    houseTypesInfo.Name = reader.GetString(reader.GetOrdinal("Name"));
                    houseTypesInfo.BaseRate = reader.GetDecimal(reader.GetOrdinal("BaseRate"));
                    houseTypesInfo.RatePerRate = reader.GetDecimal(reader.GetOrdinal("RatePerRate"));
                    houseTypesInfo.MinRooms = reader.GetInt32(reader.GetOrdinal("MinRooms"));
                    houseTypesInfo.MaxRooms = reader.GetInt32(reader.GetOrdinal("MaxRooms"));
                    houseTypesInfo.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                }
            }
            return houseTypesInfo;
        }
    }
}
