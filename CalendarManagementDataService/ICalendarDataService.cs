using CalendarManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarManagementDataService
{
    public interface ICalendarDataService
    {
        void Add(CalendarEvent calends);
        void UpdateEvents(CalendarEvent c);
        void DeleteEvents(Guid id);
        List<CalendarEvent> SearchEvents(string titlekeyword);
        CalendarEvent? ViewEvent(Guid EventId);
        List<CalendarEvent> GetCalendars();
    }
}
