using System.Net;

namespace Clicco.InvoiceServiceAPI.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await response.WriteAsJsonAsync(
                    error.Message
                );
            }
        }
    }
}
