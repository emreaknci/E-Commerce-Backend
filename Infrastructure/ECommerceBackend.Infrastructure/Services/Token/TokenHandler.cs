using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Token;
using ECommerceBackend.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using T=ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Infrastructure.Services.Token
{
    public class TokenHandler:ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public T.Token CreateAccessToken(int second)
        {
            T.Token token = new ();
            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
                //claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
            );

            //Token oluşturucu sınıf
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using var random= RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);

        }
    }
}
