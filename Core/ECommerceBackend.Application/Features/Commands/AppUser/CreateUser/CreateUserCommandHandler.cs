using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs.User;
using ECommerceBackend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using u = ECommerceBackend.Domain.Entities.Identity;
namespace ECommerceBackend.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{

    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.CreateAsync(new()
        {
            Email = request.Email,
            FullName = request.FullName,
            Password = request.Password,
            UserName = request.UserName,
            ConfirmPassword = request.ConfirmPassword,
        });
        return new()
        {
            Message = response.Message,
            Success = response.Success
        };
    }
}