using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs.User;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.Commands.AppUser.CreateUser;
using ECommerceBackend.Application.Helpers;
using ECommerceBackend.Application.Repositories.Endpoint;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEndpointReadRepository _endpointReadRepository;
        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserDto model)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,

            }, model.Password);

            CreateUserResponse response = new() { Success = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı oluşturuldu!";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n ";
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user,
            DateTime accessTokenDate, int refreshTokenLifeTimeInSeconds)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTimeInSeconds);

                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();


        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task<List<UserDtoForList>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(user => new UserDtoForList
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.FullName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName

            }).ToList();
        }

        public int TotalUsersCount => _userManager.Users.Count();

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }
        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName) ?? await _userManager.FindByNameAsync(userIdOrName);

            if (user == null) return new string[] { };
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);

            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null)
                return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);


            foreach (var userRole in userRoles)
            {
                if (endpointRoles.Any(endpointRole => userRole == endpointRole))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
