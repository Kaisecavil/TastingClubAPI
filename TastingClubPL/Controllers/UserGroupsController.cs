﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.EventDrinkDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;
using TastingClubBLL.Services;
using TastingClubBLL.ViewModels.ApplicationUserViewModels;
using TastingClubBLL.ViewModels.DrinkViewModels;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly UserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        // GET: api/UserGroup
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DrinkGeneralViewModel>))]
        public async Task<ActionResult<IEnumerable<ApplicationUserGeneralViewModel>>> GetUserGroup(int eventId)
        {
            try
            {
                return Ok(await _userGroupService.GetAllUserGroupAsync(eventId));
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


        // POST: api/UserGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> PostUserGroups(List<EventDrinkDtoForCreate> userGroupDtos)
        {
            try
            {
                var createdUserGroupId = await _userGroupService.CreateUserGroupAsync(userGroupDtos);
                return CreatedAtAction("GetUserGroup", new { id = createdUserGroupId }, createdUserGroupId);
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

        // DELETE: api/UserGroup/176223D5-5073-4961-B4EF-ECBE41F1A0C6
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        //[Authorize(Roles = RoleConstants.AdminRole)]
        public async Task<IActionResult> DeleteUserGroups(int id)
        {
            try
            {
                await _userGroupService.DeleteUserGroupAsync(id);
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