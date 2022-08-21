using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);
        if (user == null) throw new NotFoundUserException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            var token = _tokenHandler.CreateAccessToken(5);
            return new LoginUserCommandSuccessResponse()
            {
                Message = "Giriş başarılı!",
                Success = true,
                Token = token
            };
        }

        return new LoginUserCommandErrorResponse()
        {
            Success = false,
            Message = "Kullanıcı adı veya şifre hatalı!"
        };

    }
}