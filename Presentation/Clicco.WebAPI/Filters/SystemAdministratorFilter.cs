using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Clicco.WebAPI.Filters
{
    public class SystemAdministratorFilter : IAuthorizationFilter
    {
        private const string CLAIM_TYPE = "IS_ADMIN";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring(7);

                // Decode the JWT to get the claims
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);

                // Check if the custom claim exists with the specified value
                var customClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE);
                if (customClaim == null)
                {
                    // If the custom claim doesn't exist or has the wrong value, return a 401 Unauthorized response
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                // If no Authorization header was found, return a 401 Unauthorized response
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
