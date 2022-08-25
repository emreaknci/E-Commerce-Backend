using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public GoogleLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await _authService.GoogleLoginAsync(request.IdToken, 900);
        return new()
        {
            Token = token
        };
    }
}