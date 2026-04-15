using CalendarManagementDataService;
using CalendarManagementModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalendarManagement
{
    public class CalendarDBData : ICalendarDataService
    {
        private string connectionString
            = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = CalendarManagement; Integrated Security = True; TrustServerCertificate=True;";
        private SqlConnection sqlConnection;

        public CalendarDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();

        }
        private void AddSeeds()
        {
            var calends = GetCalendars();

            if (calends.Count == 0)
            {
                CalendarEvent calevent = new CalendarEvent
                {
                    EventId = Guid.NewGuid(),
                    Title = "title",
                    Date = DateTime.Today
                };
                Add(calevent);
            }
        }

        public void Add(CalendarEvent calends)
        {
            var insertStatement = "INSERT INTO TBL1 VALUES (@EventId,@Title,@Date)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@EventId", calends.EventId);
            insertCommand.Parameters.AddWithValue("@Title", calends.Title);
            insertCommand.Parameters.AddWithValue("@Date", calends.Date);
            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

        }
        public List<CalendarEvent> GetCalendars()
        {
            string selectStatement = "SELECT EventId, Title,  Date FROM TBL1";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            var calendar = new List<CalendarEvent>();

            while (reader.Read())
            {
                //deserialize

                CalendarEvent cal = new CalendarEvent();
                cal.EventId = Guid.Parse(reader["EventId"].ToString());
                cal.Title = reader["Title"].ToString();
                cal.Date = Convert.ToDateTime(reader["Date"].ToString());

                calendar.Add(cal);
            }

            sqlConnection.Close();
            return calendar;
        }
        public CalendarEvent? ViewEvent(Guid EventId)
        {
            var selectStatement = "SELECT EventId, Title,Date FROM TBL1 WHERE EventId = @EventId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@EventId", EventId.ToString());
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var cal = new CalendarEvent();

            while (reader.Read())
            {
                cal.EventId = Guid.Parse(reader["EventId"].ToString());
                cal.Title = reader["Title"].ToString();
                cal.Date = Convert.ToDateTime(reader["Date"].ToString());
            }

            sqlConnection.Close();
            return cal;
        }

        public void UpdateEvents(CalendarEvent ce)
        {

            sqlConnection.Open();

            var updateStatement = $"UPDATE TBL1 SET EventId = @EventId, Title = @Title, Date = @Date WHERE EventId = @EventId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@EventId", ce.EventId);
            updateCommand.Parameters.AddWithValue("@Title", ce.Title);
            updateCommand.Parameters.AddWithValue("@Date", ce.Date);

            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public void DeleteEvents(Guid id)
        {
            sqlConnection.Open();
            var updateStatement = $"DELETE FROM TBL1 WHERE EventId = @EventId";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@EventId", id);


            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<CalendarEvent> SearchEvents(string c) {

            var  events = new List<CalendarEvent>();
            var selectStatement = "SELECT EventId, Title,Date FROM TBL1 WHERE Title = @Title";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@Title",c.ToString());
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var cal = new CalendarEvent();

            while (reader.Read())
            {
                cal.EventId = Guid.Parse(reader["EventId"].ToString());
                cal.Title = reader["Title"].ToString();
                cal.Date = Convert.ToDateTime(reader["Date"].ToString());
                events.Add(cal);
            }

            sqlConnection.Close();
            return events;

        }

    }
}
