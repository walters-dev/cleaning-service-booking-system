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
        private readonly IServiceTypesRepository _serviceTypesRepository = new RepositoryServiceTypes();

        public ServiceTypes GetServiceTypeInput()
        {
            IServiceTypesService serviceTypesService = new ServiceTypesService(_serviceTypesRepository);

            IList<ServiceTypes> serviceTypes =
                serviceTypesService.ViewAllServiceTypes();

            Console.WriteLine();
            Console.WriteLine("===== SERVICE TYPES =====");

            if (serviceTypes.Count == 0)
            {
                throw new InvalidOperationException(
                    "No service types are available.");
            }
            int i;
            for (i = 0;
                 i < serviceTypes.Count;
                 i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{serviceTypes[i].ServiceDescription} " +
                    $"- Multiplier: " +
                    $"{serviceTypes[i].Multiplier}");
            }

            while (true)
            {
                Console.Write("Choose a service type: ");

                if (!int.TryParse(Console.ReadLine(),out int choice) || choice > i)
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");

                    continue;
                }
                return serviceTypes[choice - 1];
            }
        }
    }
}