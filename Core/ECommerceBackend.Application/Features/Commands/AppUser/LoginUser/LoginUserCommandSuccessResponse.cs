using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
{
    public Token Token { get; set; }

}