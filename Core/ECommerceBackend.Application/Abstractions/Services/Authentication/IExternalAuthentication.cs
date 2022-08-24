using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken,int accessTokenLifeTimeInSeconds);
        Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTimeInSeconds);
    }
}
