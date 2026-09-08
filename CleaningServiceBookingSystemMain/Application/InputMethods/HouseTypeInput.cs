using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystem.Application.Validators;
using CleaningServiceBookingSystemMain.Infrastructure;

namespace CleaningServiceBookingSystemMain.Application.InputMethods
{
    public class HouseTypeInput
    {
        private readonly IHouseTypesRepository _houseTypesRepository;

        private readonly BookingValidator _validator =
            new BookingValidator();

        //public HouseTypeInput(IHouseTypesRepository houseTypesRepository)
        //{
        //    _houseTypesRepository = houseTypesRepository;
        //}

        public HouseTypes GetHouseTypeInput()
        {
            InMemoryRepositoryHouseTypes inMemoryRepositoryHouseTypes = new InMemoryRepositoryHouseTypes();

            IList<HouseTypes> houseTypes = inMemoryRepositoryHouseTypes.GetHouseTypes();

            Console.WriteLine();
            Console.WriteLine("===== HOUSE TYPES =====");

            if (houseTypes.Count == 0)
            {
                throw new InvalidOperationException("No house types are available.");
            }
            int i;
            for (i = 0; i < houseTypes.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{houseTypes[i].Name} " +
                    $"- R{houseTypes[i].BaseRate}");
            }
            
            while (true)
            {
                Console.Write("Choose a house type: ");

                if (!int.TryParse(Console.ReadLine(),out int choice) ||choice > i)
                {
                    Console.WriteLine("Please enter a valid number.");

                    continue;
                }
                return houseTypes[choice - 1];
                //bool isValid =
                //    _validator.ValidateMenuChoice(choice,1,houseTypes.Count,"house type",out string errorMessage);

                //if (isValid)
                //{
                //    return houseTypes[choice - 1];
                //}

                // Console.WriteLine(errorMessage);
            }
        }
    }
}