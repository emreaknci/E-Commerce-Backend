using ECommerceBackend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);
        if (user == null) throw new NotFoundUserException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            //todo yetkiler belirlenecek
            return new()
            {
                Message = "Giriş başarılı!",
                Success = true
            };
        }

        return new()
        {
            Success = false,
            Message = "Kullanıcı adı veya şifre hatalı!"
        };

    }
}