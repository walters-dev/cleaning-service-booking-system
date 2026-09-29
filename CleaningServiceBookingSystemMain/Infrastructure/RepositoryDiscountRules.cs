using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleaningServiceBookingSystemMain.Infrastructure
{
    public class RepositoryDiscountRules : IDiscountRulesRepository
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
        public DiscountRules GetDiscountRulesById(string? Id)
        {
            DiscountRules discountRulesInfo = new DiscountRules();
            using (SqlConnection connection = new SqlConnection(databaseConnection.ConnectionString))
            {
                SqlCommand command = new SqlCommand("dbo.GetCustomer", connection);//waiting for sql procedure.......................................................................
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
    }
}
