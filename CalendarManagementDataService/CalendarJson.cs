using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CalendarManagementModels;

namespace CalendarManagementDataService
{
    public class CalendarJson:ICalendarDataService
    {
        private List<CalendarEvent> calendar = new List<CalendarEvent>();
        private string _jsonFileName;
        public CalendarJson()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/calendarj.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (calendar.Count <= 0)
            {
                calendar.Add(new CalendarEvent { EventId = Guid.NewGuid(), Title = "Birthday Party", Date = DateTime.Now });
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<CalendarEvent>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , calendar);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.calendar = JsonSerializer.Deserialize<List<CalendarEvent>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        public void Add(CalendarEvent cal)
        {
            calendar.Add(cal);
            SaveDataToJsonFile();
        }
        public List<CalendarEvent> GetCalendars()
        {
            RetrieveDataFromJsonFile();
            return calendar;
        }
        public CalendarEvent? ViewEvent(Guid Id) {
            RetrieveDataFromJsonFile();
            return calendar.FirstOrDefault(t => t.EventId == Id);

        }
    }
}
