using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Application.DTOs.Facebook;
using ECommerceBackend.Application.Exceptions;
using ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceBackend.Application.Features.Commands.AppUser.LoginUser;
using ECommerceBackend.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _httpClient = httpClientFactory.CreateClient();
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTimeInSeconds)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        FullName = name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
                else
                {
                    throw new Exception("Invalid external authentication.");
                }
            }


            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTimeInSeconds,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }

            throw new Exception("Invalid external authentication.");
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTimeInSeconds)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:ClientId"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:ClientSecret"]}&grant_type=client_credentials");

            var facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");
                var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateUserExternalAsync(user, userInfo.Email, userInfo.Name, info, accessTokenLifeTimeInSeconds);

            }
            else
                throw new Exception("Invalid external authentication.");
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTimeInSeconds)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTimeInSeconds);

        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTimeInSeconds)
        {
            var user = await _userManager.FindByNameAsync(userNameOrEmail) ?? await _userManager.FindByEmailAsync(userNameOrEmail);
            if (user == null) throw new NotFoundUserException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                var token = _tokenHandler.CreateAccessToken(accessTokenLifeTimeInSeconds,user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }

            throw new Exception("Kullanıcı adı veya şifre hatalı!");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var token = _tokenHandler.CreateAccessToken(900,user);
                await _userService.UpdateRefreshToken(refreshToken, user, token.Expiration, 300);
                return token;
            }

            throw new NotFoundUserException();

        }
    }
}
