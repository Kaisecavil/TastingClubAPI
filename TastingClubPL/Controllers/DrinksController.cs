using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.DrinkDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkService _drinkService;

        public DrinksController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        // GET: api/Drinks
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DrinkGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<DrinkGeneralViewModel>>> GetDrinks()
        {
            try
            {
                return Ok(await _drinkService.GetAllDrinksAsync());
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

        // GET: api/Drinks/1
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(DrinkDetailViewModel))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<DrinkDetailViewModel>> GetDrink(int id)
        {
            try
            {
                return Ok(await _drinkService.GetDrinkAsync(id));
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

        // PUT: api/Drinks/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PutDrink(DrinkDtoForUpdate drinkDtoDto)
        {
            try
            {
                await _drinkService.UpdateDrinkAsync(drinkDtoDto);
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

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostDrink(DrinkDtoForCreate drinkDto)
        {
            try
            {
                var createdDrinkId = await _drinkService.CreateDrinkAsync(drinkDto);
                return CreatedAtAction("GetDrink", new { id = createdDrinkId }, createdDrinkId);
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

        // DELETE: api/Drinks/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            try
            {
                await _drinkService.DeleteDrinkAsync(id);
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
