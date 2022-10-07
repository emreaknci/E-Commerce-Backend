using ECommerceBackend.Application.Abstractions.Services.Authentication;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IAuthService:IExternalAuthentication,IInternalAuthentication
{
    Task PasswordResetAsync(string email);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);

}