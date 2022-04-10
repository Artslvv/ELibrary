using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ELibrary.Services
{
    class JwtGenerator : IJwtGenerator
    {
        public JwtGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public string Create(string login, string role, string age, string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyval = Configuration["Secret"];
            var key = Encoding.ASCII.GetBytes(keyval);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                    new Claim(ClaimsIdentity.DefaultIssuer, id),
                    new Claim(ClaimTypes.DateOfBirth, age)
                }),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var res = tokenHandler.WriteToken(token);
            return res;
        }
    }
}