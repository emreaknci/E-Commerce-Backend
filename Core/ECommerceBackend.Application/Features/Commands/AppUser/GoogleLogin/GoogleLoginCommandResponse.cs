using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandResponse
{
    public string Message { get; set; }
    public bool Success { get; set; }

}