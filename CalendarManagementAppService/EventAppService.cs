using CalendarManagement;
using CalendarManagementDataService;
using CalendarManagementModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CalendarManagementAppService
{
    public class EventAppService
    {
        CalendarMemoryData eventDataService = new CalendarMemoryData();
           CalendarDataService Calendardataservice = new CalendarDataService(new CalendarDBData());
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
              Calendardataservice.Add(newEvent);
            j.Add(newEvent);
            eventDataService.AddEvent(newEvent);

            return true;
        }

        public List<CalendarEvent> GetEvents()
        {
           
                return Calendardataservice.GetCalendars();
            return j.GetCalendars();
            return eventDataService.GetEvents();
        }

        public void DeleteEvents(Guid id)
        {
            var events = Calendardataservice.GetCalendars();
            var ev = events.FirstOrDefault(t => t.EventId == id);

            if (ev != null)
            {
                events.Remove(ev);
                Calendardataservice.DeleteEvents(id);
                j.DeleteEvents(id);
            }

        }
        public void EditEvents(Guid id, DateTime newDT)
        {

            var events = Calendardataservice.GetCalendars();
            var ev = events.FirstOrDefault(t => t.EventId == id);

            if (ev != null)
            {
                ev.Date = newDT;
                Calendardataservice.UpdateEvents(ev);
                //taskjsondata.UpdateTask(task);
                //taskinmemorydata.UpdateTask(task);

            }
        }
        
        public List<CalendarEvent> SearchEvents(string titleKeyword)
        {
            return Calendardataservice.SearchEvents(titleKeyword);
        }
    }
}

