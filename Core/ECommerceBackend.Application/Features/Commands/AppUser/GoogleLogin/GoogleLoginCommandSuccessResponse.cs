using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandSuccessResponse : GoogleLoginCommandResponse
{
    public Token Token { get; set; }

}