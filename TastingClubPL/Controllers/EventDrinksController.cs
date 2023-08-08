using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.EventDrinkDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDrinksController : ControllerBase
    {
        private readonly IEventDrinkService _eventDrinksService;

        public EventDrinksController(IEventDrinkService eventDrinksService)
        {
            _eventDrinksService = eventDrinksService;
        }

        // GET: api/EventDrinks
        [HttpGet("{eventId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DrinkGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<DrinkGeneralViewModel>>> GetEventDrinks(int eventId)
        {
            try
            {
                return Ok(await _eventDrinksService.GetAllEventDrinksAsync(eventId));
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


        // POST: api/EventDrinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostEventDrinks(List<EventDrinkDtoForCreate> eventDrinksDtos)
        {
            try
            {
                var createdEventDrinksId = await _eventDrinksService.CreateEventDrinksAsync(eventDrinksDtos);
                return CreatedAtAction("GetEventDrinks", new { id = createdEventDrinksId }, createdEventDrinksId);
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

        // DELETE: api/EventDrinks/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteEventDrinks(List<int> ids)
        {
            try
            {
                await _eventDrinksService.DeleteEventDrinksAsync(ids);
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
