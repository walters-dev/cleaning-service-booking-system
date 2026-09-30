using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IDiscountRulesService
    {
        IList<DiscountRules> ViewAllDiscountRules();//returns all discount rules
        DiscountRules FindDiscountRules(string? discountId); //finds 1 discount rule
    }
}
