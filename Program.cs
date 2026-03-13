using System;
using CalendarManagementAppService;
using CalendarManagementModels;

namespace CalendarManagement
{
    class Program
    {
        static EventAppService appService = new EventAppService();

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("=== Simple Student Calendar ===");
                Console.WriteLine("1. Add Event");
                Console.WriteLine("2. View Events");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEvent();
                        break;

                    case "2":
                        ViewEvents();
                        break;

                    case "3":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        static void AddEvent()
        {
            Console.Write("Enter event title: ");
            string title = Console.ReadLine();

            Console.Write("Enter date (yyyy-mm-dd): ");
            string dateInput = Console.ReadLine();

            if (DateTime.TryParse(dateInput, out DateTime date))
            {
                bool success = appService.AddEvent(title, date);

                if (success)
                    Console.WriteLine("Event added!");
                else
                    Console.WriteLine("Invalid event title.");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }
        }

        static void ViewEvents()
        {
            var events = appService.GetEvents();

            if (events.Count == 0)
            {
                Console.WriteLine("No events yet!");
                return;
            }

            Console.WriteLine("\nYour Events:");

            foreach (var ev in events)
            {
                Console.WriteLine($"{ev.Date.ToShortDateString()} - {ev.Title}");
            }
        }
    }
}