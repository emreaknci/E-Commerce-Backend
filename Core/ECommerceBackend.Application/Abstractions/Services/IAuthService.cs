using ECommerceBackend.Application.Abstractions.Services.Authentication;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IAuthService:IExternalAuthentication,IInternalAuthentication
{


}