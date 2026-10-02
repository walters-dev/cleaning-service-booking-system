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
        private readonly IHouseTypesRepository _houseTypesRepository; 
        public HouseTypeInput(IHouseTypesRepository houseTypesRepository)
        {
            _houseTypesRepository = houseTypesRepository;
        }
        public HouseTypes GetHouseTypeInput()
        {
           
            
            IHouseTypeService service = new HouseTypeService(_houseTypesRepository);

            IList<HouseTypes> houseTypes = service.ViewAllHouseTypes();

            Console.WriteLine();
            Console.WriteLine("===== HOUSE TYPES =====");

            HouseTypes selectedHouseType = AnsiConsole.Prompt(
                  new SelectionPrompt<HouseTypes>()
                      .Title("[yellow]Choose a house type:[/]")
                      .HighlightStyle(new Style(Color.Green))
                      .UseConverter(houseType =>$"{houseType.Name} - R{houseType.BaseRate}")
                      .AddChoices(houseTypes)

                    
            );
            Console.WriteLine("Selected house type: "+ selectedHouseType.Name);
            return selectedHouseType;
        }
    }
}