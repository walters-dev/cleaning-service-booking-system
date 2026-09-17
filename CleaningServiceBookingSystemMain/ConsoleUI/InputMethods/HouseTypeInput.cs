using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class HouseTypeInput
    {
        private readonly IHouseTypesRepository _houseTypesRepository = new InMemoryRepositoryHouseTypes();

        private readonly BookingValidator _validator =
            new BookingValidator();

        //public HouseTypeInput(IHouseTypesRepository houseTypesRepository)
        //{
        //    _houseTypesRepository = houseTypesRepository;
        //}

        public HouseTypes GetHouseTypeInput()
        {
            HouseTypeService service = new HouseTypeService(_houseTypesRepository);

            IList<HouseTypes> houseTypes = service.ViewAllHouseTypes();

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