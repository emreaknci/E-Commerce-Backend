using System.Text.Json;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;

public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
{
    readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    readonly ITokenHandler _tokenHandler;
    readonly HttpClient _httpClient;
    public FacebookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory)
    {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClientFactory.CreateClient();
    }
    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
    {
        string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=2025229251020676&client_secret=e531253d5c100cbbd8ecd08794630007&grant_type=client_credentials");

        var facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
        string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

        var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
        if (validation.Data.IsValid)
        {
            string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");
            var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userInfo.Email);
                if (user == null)
                {
                    var found = userInfo.Email.IndexOf("@");
                    var userName = userInfo.Email.Substring(0, found);
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = userInfo.Email,
                        UserName = userName,
                        FullName = userInfo.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
                else
                {
                    return new FacebookLoginCommandErrorResponse()
                    {
                        Success = false,
                        Message = "Bu mail adresi daha önce kullanılmış"
                    };
                }
            }
           

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins
                Token token = _tokenHandler.CreateAccessToken(5);
                return new FacebookLoginCommandSuccessResponse()
                {
                    Token = token,
                    Success = true,
                    Message = "Facebook ile giriş yapıldı."
                };
            }


           
        }

        return new FacebookLoginCommandErrorResponse()
        {
            Success = false,
            Message = "Facebook ile oturum açma işlemi gerçekleştirilemedi."
        };


    }
}