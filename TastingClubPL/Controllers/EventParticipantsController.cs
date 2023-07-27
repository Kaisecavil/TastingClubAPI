using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.EventDrinkDTOs;
using TastingClubBLL.DTOs.EventParticipantDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantsController : ControllerBase
    {
        private readonly IEventParticipantService _eventParticipantService;

        public EventParticipantsController(IEventParticipantService eventParticipantService)
        {
            _eventParticipantService = eventParticipantService;
        }

        // GET: api/EventParticipant
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DrinkGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserGeneralViewModel>>> GetEventParticipant(int eventId)
        {
            try
            {
                return Ok(await _eventParticipantService.GetAllEventParticipantAsync(eventId));
            }
            catch (HttpStatusException httpStatusException)
            {
                return httpStatusException.Status switch
                {
                    HttpStatusCode.NotFound => NotFound(httpStatusException.Message),
                    _ => BadRequest()
                };
            }
        }


        // POST: api/EventParticipant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostEventParticipants(List<EventParticipantDtoForCreate> eventParticipantDtos)
        {
            try
            {
                var createdEventParticipantId = await _eventParticipantService.CreateEventParticipantAsync(eventParticipantDtos);
                return CreatedAtAction("GetEventParticipant", new { id = createdEventParticipantId }, createdEventParticipantId);
            }
            catch (HttpStatusException httpStatusException)
            {
                return httpStatusException.Status switch
                {
                    HttpStatusCode.NotFound => NotFound(httpStatusException.Message),
                    _ => throw httpStatusException
                };
            }
        }

        // DELETE: api/EventParticipant/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteEventParticipants(List<int> ids)
        {
            try
            {
                await _eventParticipantService.DeleteEventParticipantAsync(ids);
                return NoContent();
            }
            catch (HttpStatusException httpStatusException)
            {
                return httpStatusException.Status switch
                {
                    HttpStatusCode.NotFound => NotFound(httpStatusException.Message),
                    _ => throw httpStatusException
                };
            }
        }
    }
}
