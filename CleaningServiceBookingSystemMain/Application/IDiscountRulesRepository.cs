using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IDiscountRulesRepository
    {
        IList<DiscountRules> GetDiscountRules();    //gets list of all Discount Rules from storage
        DiscountRules GetDiscountRulesById(int? Id);//gets a specific Discount Rule from storage
        void Add(DiscountRules discountRules);      //Adds a Discount Rule to storage
        //add update and delete if have time
    }
}
