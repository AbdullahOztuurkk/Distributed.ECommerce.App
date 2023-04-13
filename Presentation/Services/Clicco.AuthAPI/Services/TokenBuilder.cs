using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clicco.AuthAPI.Services
{
    public class TokenHandler : ITokenHandler<User>
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Token üretecek metot.
        public string CreateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Configuration["AUTH_API_KEY"]);
            var claimIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, string.Join(' ',user.FirstName,user.LastName)),
            });

            if (user.IsSA)
            {
                claimIdentity.AddClaim(new Claim("IS_ADMIN", user.IsSA.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
