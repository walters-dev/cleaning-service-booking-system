using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IDiscountRulesRepository
    {
        IList<DiscountRules> GetDiscountRules();
        DiscountRules GetDiscountRulesById(int? Id);
        void Add(DiscountRules discountRules);
        //add update and delete if have time
    }
}
