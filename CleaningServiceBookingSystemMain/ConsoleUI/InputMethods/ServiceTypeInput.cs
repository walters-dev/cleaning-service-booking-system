using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Interfaces;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class ServiceTypeInput
    {
        private readonly IServiceTypesRepository
            _serviceTypesRepository;

        private readonly BookingValidator _validator =
            new BookingValidator();

        //public ServiceTypeInput(IServiceTypesRepository serviceTypesRepository)
        //{
        //    _serviceTypesRepository =
        //        serviceTypesRepository;
        //}

        public ServiceTypes GetServiceTypeInput()
        {
            InMemoryRepositoryServiceTypes inMemoryRepositoryServiceTypes = new InMemoryRepositoryServiceTypes();

            IList<ServiceTypes> serviceTypes =
                inMemoryRepositoryServiceTypes.GetServiceTypes();

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
                    Console.WriteLine("Please enter a valid number.");

                    continue;
                }
                return serviceTypes[choice - 1];
                //bool isValid =_validator.ValidateMenuChoice(choice, 1, serviceTypes.Count, "service type",out string errorMessage);

                //if (isValid)
                //{
                //    return serviceTypes[choice - 1];
                //}

                // Console.WriteLine(errorMessage);
            }
        }
    }
}