
using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using ECommerceBackend.Application.Features.Commands.AppUser.AssignRoleToUser;
using ECommerceBackend.Application.Features.Commands.AppUser.CreateUser;
using ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;
using ECommerceBackend.Application.Features.Commands.AppUser.UpdatePassword;
using ECommerceBackend.Application.Features.Queries.AppUser.GetAllUsers;
using ECommerceBackend.Application.Features.Queries.AppUser.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await Mediator!.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await Mediator!.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await Mediator!.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }
    }
}
