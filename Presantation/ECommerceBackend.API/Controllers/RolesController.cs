using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using ECommerceBackend.Application.Features.Commands.Role.CreateRole;
using ECommerceBackend.Application.Features.Commands.Role.DeleteRole;
using ECommerceBackend.Application.Features.Commands.Role.UpdateRole;
using ECommerceBackend.Application.Features.Queries.Role.GetRoleById;
using ECommerceBackend.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : BaseController
    {


        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            var response = await Mediator!.Send(getRolesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            var response = await Mediator!.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            var response = await Mediator!.Send(createRoleCommandRequest);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            var response = await Mediator!.Send(updateRoleCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            var response = await Mediator!.Send(deleteRoleCommandRequest);
            return Ok(response);
        }
    }
}

