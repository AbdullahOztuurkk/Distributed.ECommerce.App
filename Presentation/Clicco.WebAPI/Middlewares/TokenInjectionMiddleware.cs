using Microsoft.AspNetCore.Authorization;

namespace Clicco.WebAPI.Middlewares
{
    public class TokenInjectionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            var authorizeAttribute = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();
            if (authorizeAttribute != null)
            {
                // Check the Authorization header
                if (context.Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
                {
                    var authHeaderValue = authHeaderValues.ToString();
                    if (authHeaderValue.StartsWith("Bearer "))
                    {
                        // Get current token
                        var jwt = authHeaderValue.Substring("Bearer ".Length);

                        // add token as header to current request
                        context.Request.Headers.Add("Authorization", new[] { $"Bearer {jwt}" });
                    }
                }
            }

            await next(context);
        }
    }
}
