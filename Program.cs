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
                Console.WriteLine("3. Delete Event");
                Console.WriteLine("4. Edit Events");
                Console.WriteLine("5. Search Events");
                Console.WriteLine("6. Exit");
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
                        DeleteEvent();
                        break;

                    case "4":
                        UpdateEvent();
                        break;

                   case "5":
                      SearchEvent();
                        break;
                    case "6":
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
        static void DeleteEvent()
        {
            var events = appService.GetEvents();

            if (events.Count == 0)
            {
                Console.WriteLine("No events available.");
                Console.WriteLine("-------------------------");
                return;
            }

            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {events[i].Title}");
            }

            Console.Write("Select event number to delete: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= events.Count)
            {
                Console.WriteLine("Invalid selection.");

                Console.WriteLine("-------------------------");
                return;
            }

            Guid selectedId = events[index].EventId;

            appService.DeleteEvents(selectedId);

            Console.WriteLine("Task deleted!");
            Console.WriteLine("-------------------------");
        }
        static void UpdateEvent()
        {
            var ev = appService.GetEvents();

            if (ev.Count == 0)
            {
                Console.WriteLine("No events available.");
                Console.WriteLine("-------------------------");
                return;
            }


            for (int i = 0; i < ev.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ev[i].Title}");
            }

            Console.Write("Select event number to update: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= ev.Count)
            {
                Console.WriteLine("Invalid selection.");
                Console.WriteLine("-------------------------");
                return;
            }


            Guid selectedId = ev[index].EventId;

            Console.Write("Enter new task name: ");
            DateTime newDT = Convert.ToDateTime(Console.ReadLine());

            appService.EditEvents(selectedId, newDT);

            Console.WriteLine("Event updated!");
            Console.WriteLine("-------------------------");
        }

        static void SearchEvent()
        {
            Console.WriteLine("=== SEARCH EVENT BY TITLE ===");
            Console.Write("Enter event title keyword: ");
            string keyword = Console.ReadLine();

            List<CalendarEvent> results = appService.SearchEvents(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("No event found.");
            }
            else
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var e in results)
                {
                    Console.WriteLine($"ID   : {e.EventId}");
                    Console.WriteLine($"Title: {e.Title}");
                    Console.WriteLine($"Date : {e.Date}");
                    Console.WriteLine("-------------------");
                }
            }
        }
        }
    }
    