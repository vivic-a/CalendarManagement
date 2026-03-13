using System;

namespace CalendarManagementModels
{
    public class CalendarEvent
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

    }
}
