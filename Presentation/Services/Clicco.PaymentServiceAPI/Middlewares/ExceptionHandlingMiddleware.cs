using System.Net;

namespace Clicco.PaymentServiceAPI.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
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

                await response.WriteAsync(error.Message);
            }
        }
    }
}
