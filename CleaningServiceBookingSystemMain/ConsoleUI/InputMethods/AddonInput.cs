using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Domain.Models;
using Spectre.Console;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class AddOnInput
    {
        private readonly IAddOnsRepository _addOnsRepository;

        public AddOnInput(IAddOnsRepository addOnsRepository)
        {
            this._addOnsRepository = addOnsRepository;
        }

        private const int MaxRoomNumber = 12;

        public IList<AddOnSelection>? GetAddOnInput(
            ref int carpetedRooms)
        {
            // Ask the user whether they want any add-ons.
            string addOnChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Would you like to add any add-ons?[/]")
                    .AddChoices(
                        "Yes",
                        "No"
                    )
            );

            // If the user does not want add-ons,
            // return an empty list.
            if (addOnChoice == "No")
            {
                return new List<AddOnSelection>();
            }


            // Create the service using the repository.
            AddOnsService service =
                new AddOnsService(_addOnsRepository);

            // Get all available add-ons.
            IList<AddOns> addOns =
                service.ViewAllAddOns();

            Console.WriteLine();
            Console.WriteLine("===== ADD-ONS =====");

            // Allow the user to select multiple add-ons.
            List<AddOns> selectedAddOns =
                AnsiConsole.Prompt(
                    new MultiSelectionPrompt<AddOns>()
                        .Title("[yellow]Choose your add-ons:[/]")
                        .InstructionsText(
                            "[grey](Press [blue]<space>[/] to select, " +
                            "[green]<enter>[/] when finished)[/]")
                        .UseConverter(addOn =>$"{addOn.AddOnsName} - R{addOn.Rate}")
                        .AddChoices(addOns)
                );

            // This list will store the selected add-ons
            // together with their quantities.
            List<AddOnSelection> selections =
                new List<AddOnSelection>();

            // Go through every add-on selected by the user.
            foreach (AddOns selectedAddOn in selectedAddOns)
            {
                // Most add-ons only have a quantity of 1.
                int quantity = 1;

                // AD002 = Carpet Cleaning.
                if (selectedAddOn.AddOnId == "AD002")
                {
                    while (true)
                    {
                        Console.Write(
                            "Enter number of carpeted rooms: ");

                        if (!int.TryParse(Console.ReadLine(), out carpetedRooms))
                        {
                            AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");

                            continue;
                        }

                        if (carpetedRooms < 1 || carpetedRooms > MaxRoomNumber)
                        {
                            AnsiConsole.MarkupLine("[red]Carpeted rooms must be between 1 and 12.[/]");

                            continue;
                        }

                        // The quantity of Carpet Cleaning
                        // is the number of carpeted rooms.
                        quantity = carpetedRooms;

                        break;
                    }
                }

                // Create an AddOnSelection object.
                AddOnSelection selection =
                    new AddOnSelection
                    {
                        AddOn = selectedAddOn,
                        Quantity = quantity
                    };

                // Add it to the list.
                selections.Add(selection);

                Console.WriteLine(
                    $"{selectedAddOn.AddOnsName} added.");
            }

            return selections;
        }
    }
}