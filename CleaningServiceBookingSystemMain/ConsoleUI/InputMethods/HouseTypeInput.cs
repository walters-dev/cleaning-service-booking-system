using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class HouseTypeInput
    {
        private readonly IHouseTypesRepository _houseTypesRepository = new RepositoryHouseTypes();//bcs its in bookinginput it doesnt like constructors

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
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");

                    continue;
                }
                return houseTypes[choice - 1];
            }
        }
    }
}