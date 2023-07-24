using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.GroupDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.ViewModels.GroupViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/Groups
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GroupGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<GroupGeneralViewModel>>> GetGroups()
        {
            try
            {
                return Ok(await _groupService.GetAllGroupsAsync());
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

        // GET: api/Groups/1
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(GroupDetailViewModel))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GroupDetailViewModel>> GetGroup(int id)
        {
            try
            {
                return Ok(await _groupService.GetGroupAsync(id));
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

        // PUT: api/Groups/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PutGroup(GroupDtoForUpdate groupDtoDto)
        {
            try
            {
                await _groupService.UpdateGroupAsync(groupDtoDto);
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostGroup(GroupDtoForCreate groupDto)
        {
            try
            {
                var createdGroupId = await _groupService.CreateGroupAsync(groupDto);
                return CreatedAtAction("GetGroup", new { id = createdGroupId }, createdGroupId);
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

        // DELETE: api/Groups/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            try
            {
                await _groupService.DeleteGroupAsync(id);
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
