using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
    public Token Token { get; set; }
}