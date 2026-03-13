using System;
using System.Collections.Generic;
using CalendarManagementModels;
using CalendarManagementDataService;

namespace CalendarManagementAppService
{
    public class EventAppService
    {
        EventDataService eventDataService = new EventDataService();

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

            return true;
        }

        public List<CalendarEvent> GetEvents()
        {
            return eventDataService.GetEvents();
        }
    }
}
