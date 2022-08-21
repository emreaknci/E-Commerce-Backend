using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;

public class FacebookLoginCommandSuccessResponse:FacebookLoginCommandResponse
{
    public Token Token { get; set; }
}