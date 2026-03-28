using System;
using System.Collections.Generic;
using CalendarManagementModels;
using CalendarManagementDataService;
using CalendarManagement;

namespace CalendarManagementAppService
{
    public class EventAppService
    {
        CalendarMemoryData eventDataService = new CalendarMemoryData();
   //   CalendarDataService Calendardataservice = new CalendarDataService(new CalendarDBData());
        CalendarJson j = new CalendarJson();
        public bool AddEvent(string title, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(title))
                return false;

            CalendarEvent newEvent = new CalendarEvent
            {
                EventId = Guid.NewGuid(),
                Title = title,
                Date = date
            };

            eventDataService.AddEvent(newEvent);
          //  Calendardataservice.Add(newEvent);
            j.Add(newEvent);

            return true;
        }

        public List<CalendarEvent> GetEvents()
        {
            return eventDataService.GetEvents();
       //    return Calendardataservice.GetCalendars();
            return j.GetCalendars();
        }
    }
}
