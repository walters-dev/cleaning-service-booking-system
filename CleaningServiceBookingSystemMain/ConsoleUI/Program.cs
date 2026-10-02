using Spectre;
using Spectre.Console;
using System;
using System.Text;

namespace CleaningServiceBookingSystemMain.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //sets encoding to UTF8 so that the spinner can have a different style
            System.Console.OutputEncoding = Encoding.UTF8;
            System.Console.InputEncoding = Encoding.UTF8;
            //Declare variables and initialization
            bool IsUserSelected = false;
            while (IsUserSelected == false)
            {
                var menuChoices = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose menu option")
                .AddChoices("Booking Administrator", "Operations Manager", "Terminate application"));   //menu options output
                switch (menuChoices)
                {
                    case "Booking Administrator"://create Booking Administrator from menu options
                        Console.WriteLine("Booking Administrator is selected");
                        Console.Clear();
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.ViewAdminMenu();
                        break;
                    case "Operations Manager"://create Booking Administrator from menu options
                        Console.WriteLine("Operations Manager is selected");
                        Console.Clear();
                        ManagerMenu managerMenu = new ManagerMenu();
                        managerMenu.ViewManagerMenu();
                        break;
                    case "Terminate application"://create Booking Administrator from menu options
                        //loads the font for the figlet
                        var font = FigletFont.Load("C:\\Users\\RPS3\\Documents\\Projects\\CleaningServiceBookingSystem\\CleaningServiceBookingSystemMain\\figlet-fonts-main\\DOS Rebel.flf");
                        var centerAligned = new FigletText(font, "Bye")
                        {
                            Justification = Justify.Center//centres figlet
                        };
                        AnsiConsole.Write(centerAligned);//diplays figlet centred
                        AnsiConsole.Status()
                            .Spinner(Spinner.Known.Dots)
                            .SpinnerStyle(Style.Parse("lightgreen"))
                            .Start("Terminating appllication...", ctx =>
                            {
                                Thread.Sleep(3000);
                            });//shows spinner/throbber untill applicatio close
                        IsUserSelected = true;                          //stops menu loop which stops application
                        break;
                }

            }


        }
    }
}