using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Application.Interfaces;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class DiscountInput
    {
        private readonly IDiscountRulesRepository
            _discountRulesRepository;

        private readonly BookingValidator _validator =
            new BookingValidator();

        public DiscountInput(IDiscountRulesRepository discountRulesRepository)
        {
            _discountRulesRepository = discountRulesRepository;
        }

        public DiscountRules? GetDiscountInput()
        {
            IList<DiscountRules> discountRules = _discountRulesRepository.GetDiscountRules();

            Console.WriteLine();
            Console.WriteLine("===== DISCOUNT RULES =====");
            Console.WriteLine("0. No Discount");
            int i;
            for (i = 0;i < discountRules.Count;i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{discountRules[i].Name} " +
                    $"- {discountRules[i].DisPercentage:P0}");
            }

            while (true)
            {
                Console.Write("Choose a discount: ");

                if (!int.TryParse(Console.ReadLine(),out int choice) || choice > i)
                {
                    Console.WriteLine("Please enter a valid number.");

                    continue;
                }

                //bool isValid =_validator.ValidateMenuChoice(choice,0, discountRules.Count, "discount", out string errorMessage);

                //if (!isValid)
                //{
                //    Console.WriteLine(errorMessage);
                //    continue;
                //}

                //if (choice == 0)
                //{
                //    return null;
                //}

                return discountRules[choice - 1];
            }
        }
    }
}