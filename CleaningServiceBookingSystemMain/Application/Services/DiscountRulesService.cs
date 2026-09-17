using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class DiscountRulesService
    {
        private readonly IDiscountRulesRepository _discountRulesRepository;
        public DiscountRulesService(IDiscountRulesRepository repository)
        {
            _discountRulesRepository = repository;
        }
        public IList<DiscountRules> ViewAllDiscountRules()
        {
            return _discountRulesRepository.GetDiscountRules();
        }
        public DiscountRules FindDiscountRules(string? discountId)
        {
            return _discountRulesRepository.GetDiscountRulesById(discountId);
        }
        public void RegisterDiscountRule(DiscountRules discountRules)
        {
            _discountRulesRepository.Add(discountRules);
        }
        public string FindDiscountCount()
        {
            return _discountRulesRepository.DiscountRulesRowCount();
        }
    }
}
