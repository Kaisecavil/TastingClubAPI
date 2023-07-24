using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.UserDrinkReviewDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.UserDrinkReviewViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDrinkReviewsController : ControllerBase
    {
        private readonly IUserDrinkReviewService _userDrinkReviewService;

        public UserDrinkReviewsController(IUserDrinkReviewService userDrinkReviewService)
        {
            _userDrinkReviewService = userDrinkReviewService;
        }

        // GET: api/UserDrinkReviews
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDrinkReviewGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<UserDrinkReviewGeneralViewModel>>> GetUserDrinkReviews()
        {
            try
            {
                return Ok(await _userDrinkReviewService.GetAllUserDrinkReviewsAsync());
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

        // GET: api/UserDrinkReviews/1
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDrinkReviewDetailViewModel))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDrinkReviewDetailViewModel>> GetUserDrinkReview(int id)
        {
            try
            {
                return Ok(await _userDrinkReviewService.GetUserDrinkReviewAsync(id));
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

        // PUT: api/UserDrinkReviews/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PutUserDrinkReview(UserDrinkReviewDtoForUpdate userDrinkReviewDto)
        {
            try
            {
                await _userDrinkReviewService.UpdateUserDrinkReviewAsync(userDrinkReviewDto);
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

        // POST: api/UserDrinkReviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostUserDrinkReview(UserDrinkReviewDtoForCreate userDrinkReviewDto)
        {
            try
            {
                var createdUserDrinkReviewId = await _userDrinkReviewService.CreateUserDrinkReviewAsync(userDrinkReviewDto);
                return CreatedAtAction("GetUserDrinkReview", new { id = createdUserDrinkReviewId }, createdUserDrinkReviewId);
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

        // DELETE: api/UserDrinkReviews/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteUserDrinkReview(int id)
        {
            try
            {
                await _userDrinkReviewService.DeleteUserDrinkReviewAsync(id);
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
