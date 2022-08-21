using ECommerceBackend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using u = ECommerceBackend.Domain.Entities.Identity;
namespace ECommerceBackend.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<u.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<u.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.UserName,
            Email = request.Email,
            FullName = request.FullName,

        }, request.Password);
        CreateUserCommandResponse response = new() { Success = result.Succeeded };
        if (result.Succeeded)
            response.Message = "Kullanıcı oluşturuldu!";
        else
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n ";

        return response;
    }
}