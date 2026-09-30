using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryHouseTypes : IHouseTypesRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<HouseTypes> GetHouseTypes()
        {
            List<HouseTypes> houseTypesInfo = new List<HouseTypes>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetAllHouseTypes", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    var houseTypes = new HouseTypes()
                    {
                        HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypesid")),
                        Name = reader.GetString(reader.GetOrdinal("HouseName")),
                        BaseRate = reader.GetDecimal(reader.GetOrdinal("BaseRate")),
                        RatePerRoom = reader.GetDecimal(reader.GetOrdinal("RatePerRoom")),
                        MinRooms = reader.GetInt32(reader.GetOrdinal("MinRooms")),
                        MaxRooms = reader.GetInt32(reader.GetOrdinal("MaxRooms"))
                    };
                    houseTypesInfo.Add(houseTypes);
                }
            }
            return houseTypesInfo;
        }
        public HouseTypes GetHouseTypesById(string? Id)
        {
            HouseTypes houseTypesInfo = new HouseTypes();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))//sets up connection to database
            {
                SqlCommand command = new SqlCommand("dbo.GetHouseType", connection);//gets stored procedure from database
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Parameters.AddWithValue("@HouseTypeId", Id);
                SqlDataReader reader = command.ExecuteReader();//executes the query and reads output
                while (reader.Read())
                {
                    houseTypesInfo.HouseTypeId = reader.GetString(reader.GetOrdinal("HouseTypesid"));
                    houseTypesInfo.Name = reader.GetString(reader.GetOrdinal("HouseName"));
                    houseTypesInfo.BaseRate = reader.GetDecimal(reader.GetOrdinal("BaseRate"));
                    houseTypesInfo.RatePerRoom = reader.GetDecimal(reader.GetOrdinal("RatePerRoom"));
                    houseTypesInfo.MinRooms = reader.GetInt32(reader.GetOrdinal("MinRooms"));
                    houseTypesInfo.MaxRooms = reader.GetInt32(reader.GetOrdinal("MaxRooms"));
                }
            }
            return houseTypesInfo;
        }
    }
}
