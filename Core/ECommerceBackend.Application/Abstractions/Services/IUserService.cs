using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.DTOs.User;
using ECommerceBackend.Domain.Entities.Identity;

namespace ECommerceBackend.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUserDto model);
        Task UpdateRefreshToken(string refreshToken,AppUser user,
            DateTime accessTokenDate,int refreshTokenLifeTimeInSeconds);

        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<UserDtoForList>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userId);
    }
}
