using Dumptruck_v4.Models;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Dumptruck_v4.Services {
    public class TokenService { // This should really have an interface

        public string CreateJwt(ScoobyUser scoob) {
            var issuer = "mywebsite"; //Get from configuration/appsetting/aws parameter store
            var audience = "mywebsite";
            var key = "The Secret has to be longer than x characters also you have lost some info here that was not saved";
            var byteKey = Encoding.UTF8.GetBytes(key);
            var tokenDescriptior = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, scoob.UserName),
                    //new Claim(JwtRegisteredClaimNames.Email, scoob.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60), // This should be in appsettings/config or other
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptior);
            //cache this token somewhere, somohow??
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token); //Otherwise these will be the same??
            return stringToken;                        
        }
    }
}
