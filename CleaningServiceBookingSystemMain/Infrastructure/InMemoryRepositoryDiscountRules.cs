using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class InMemoryRepositoryDiscountRules : IDiscountRulesRepository
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        public IList<DiscountRules> GetDiscountRules()
        {
            List<DiscountRules> discountRulesInfo = new List<DiscountRules>();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("fghj", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var discountRules = new DiscountRules()
                    {
                        DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRuleId")),
                        Name = reader.GetString(reader.GetOrdinal("DiscountName")),
                        DisPercentage = reader.GetDecimal(reader.GetOrdinal("DiscPercentage")),
                        CriteriaDescription = reader.GetString(reader.GetOrdinal("CriteriaDescription")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                    };
                    discountRulesInfo.Add(discountRules);
                }
            }
            return discountRulesInfo;
        }
        public DiscountRules GetDiscountRulesById(int? Id)
        {
            DiscountRules discountRulesInfo = new DiscountRules();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("GetCustomer", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    discountRulesInfo.DiscountRuleId = reader.GetString(reader.GetOrdinal("DiscountRuleId"));
                    discountRulesInfo.Name = reader.GetString(reader.GetOrdinal("Name"));
                    discountRulesInfo.DisPercentage = reader.GetDecimal(reader.GetOrdinal("DisPercentage"));
                    discountRulesInfo.CriteriaDescription = reader.GetString(reader.GetOrdinal("CriteriaDescription"));
                    discountRulesInfo.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                }
            }
            return discountRulesInfo;
        }
        public void Add(DiscountRules discountRules)
        {
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("AddBooking", connection);//waiting for sql procedure.......................................................................
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                //need to add a thing for id
                command.Parameters.AddWithValue("@DiscountRuleId", discountRules.DiscountRuleId);
                command.Parameters.AddWithValue("@Name", discountRules.Name);
                command.Parameters.AddWithValue("@DisPercentage", discountRules.DisPercentage);
                command.Parameters.AddWithValue("@CriteriaDescription", discountRules.CriteriaDescription);
                command.Parameters.AddWithValue("@IsActive", discountRules.IsActive);
                command.ExecuteNonQuery();
            }
        }
    }
}
