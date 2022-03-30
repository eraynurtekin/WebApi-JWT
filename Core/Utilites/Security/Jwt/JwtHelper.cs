using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilites.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilites.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions; //Token bilgileri buradan okunuyor...

        DateTime _accessTokenExpiration;
        
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        } // Konfigürasyon dosyası vasıtasıyla token option bilgilerini (appsetings bilgilerini configurasyon yapısıyla okuyacağız.)

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //Bir algoritma kullanırken kednimize şifreli bir token oluşturacağız kendi bildiğimiz bir anahtara ihtiyacımz var.
            var securityKey = SecurityKeyHelper.CreatesSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            //Bize web token üretiyor.
            var jwt = CreateJwtSecurityToken(_tokenOptions,user,signingCredentials,operationClaims);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt); //String e çevirdik WriteToken ile

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,User user, 
            SigningCredentials signingCredentials,List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(

                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:setClaims(user, operationClaims),
                signingCredentials:signingCredentials

         );
            return jwt;
        }
        
        private IEnumerable<Claim> setClaims(User user,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }

    }
}
