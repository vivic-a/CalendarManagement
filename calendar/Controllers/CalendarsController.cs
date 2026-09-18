using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CalendarManagementAppService;
using CalendarManagementModels;
using Microsoft.AspNetCore.Mvc;
using calendar.Models;

namespace calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase
    {
        private readonly EventAppService _appservice;

        public CalendarsController()
        {
            _appservice = new EventAppService();
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<CalendarEvent>> GetEvents()
        {
            var calendar = _appservice.GetEvents();
            return Ok(calendar);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] CalendarViewModel calendar)
        {
            if (calendar == null)
            {
                return BadRequest("Date data is required.");
            }

            bool created = _appservice.AddEvent(calendar.Title, calendar.Date);

            if (!created)
            {
                return BadRequest("Failed to create event.");
            }

            return Ok("Event created successfully.");
        }


        [HttpPatch("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] CalendarViewModel calen)
        {
            if (calen == null)
            {
                return BadRequest("Calendar data is required.");
            }

            var existingDate = _appservice.GetEvents().FirstOrDefault(i => i.EventId == id);

            if (existingDate == null)
            {
                return NotFound();
            }

            _appservice.EditEvents(id, calen.Date);

            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteAccount(Guid id)
        {
            var existingDate = _appservice.GetEvents();

            if (existingDate == null)
            {
                return NotFound();
            }

            _appservice.DeleteEvents(id);

            return NoContent();
        }
    }
}
