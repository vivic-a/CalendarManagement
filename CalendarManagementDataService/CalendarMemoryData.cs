using System;
using System.Collections.Generic;
using CalendarManagementModels;

namespace CalendarManagementDataService
{
    public class CalendarMemoryData
    {
        private List<CalendarEvent> events = new List<CalendarEvent>();

        public void AddEvent(CalendarEvent calendarEvent)
        {
            events.Add(calendarEvent);
        }

        public List<CalendarEvent> GetEvents()
        {
            return events;
        }
    }
}
