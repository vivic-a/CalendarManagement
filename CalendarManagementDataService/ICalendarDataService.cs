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
    
        CalendarEvent? ViewEvent(Guid EventId);
        List<CalendarEvent> GetCalendars();
    }
}
