﻿
using ECommerceBackend.Application.Features.Commands.AppUser.CreateUser;
using ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
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

       
    }
}
