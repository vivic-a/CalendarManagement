using System;
using CalendarManagementModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarManagementDataService
{
    public class CalendarDataService
    {
        ICalendarDataService dataservice;

        public CalendarDataService (ICalendarDataService icalendardataservice)
        {
            dataservice = icalendardataservice;
        }

        public void Add(CalendarEvent calends)
        {
            dataservice.Add(calends);
        }
        public void UpdateEvents(CalendarEvent c)
        {
            dataservice.UpdateEvents(c);
        }
        public void DeleteEvents(Guid id)
        {
            dataservice.DeleteEvents(id);
        }
        public List<CalendarEvent> SearchEvents(string titlekeyword)
        {
            return dataservice.SearchEvents(titlekeyword);
        }
     
        public List<CalendarEvent> GetCalendars()
        {
            return dataservice.GetCalendars();
        }
    
        
     
}
}
