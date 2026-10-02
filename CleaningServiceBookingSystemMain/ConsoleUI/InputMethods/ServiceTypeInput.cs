using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class ServiceTypeInput
    {
        private readonly IServiceTypesRepository _serviceTypesRepository;
        public ServiceTypeInput(IServiceTypesRepository serviceTypesRepository)
        {
            _serviceTypesRepository = serviceTypesRepository;
        }

        public ServiceTypes GetServiceTypeInput()
        {
            IServiceTypesService serviceTypesService = new ServiceTypesService(_serviceTypesRepository);

            IList<ServiceTypes> serviceTypes =
                serviceTypesService.ViewAllServiceTypes();

            Console.WriteLine();
            Console.WriteLine("===== SERVICE TYPES =====");

            ServiceTypes selectedServiceType = AnsiConsole.Prompt(
               new SelectionPrompt<ServiceTypes>()
                   .Title("[yellow]Choose a service type:[/]")
                   .PageSize(10)
                   .HighlightStyle(new Style(Color.Green))
                   .UseConverter(serviceType =>
                       $"{serviceType.ServiceDescription} - Multiplier: {serviceType.Multiplier}x")
                   .AddChoices(serviceTypes)
           );
            Console.WriteLine("Selected service type: " + selectedServiceType.ServiceName);
            // Return the service type selected by the user.
            return selectedServiceType;

        }
    }
}