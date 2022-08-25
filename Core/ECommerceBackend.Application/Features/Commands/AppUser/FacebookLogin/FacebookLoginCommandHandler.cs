using System.Text.Json;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;

public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public FacebookLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {

        var token= await _authService.FacebookLoginAsync(request.AuthToken, 900);
        return new()
        {
            Token = token
        };

    }
}