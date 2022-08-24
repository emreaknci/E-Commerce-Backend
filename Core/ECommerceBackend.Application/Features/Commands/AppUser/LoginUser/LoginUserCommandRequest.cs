using ECommerceBackend.Application.DTOs;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}