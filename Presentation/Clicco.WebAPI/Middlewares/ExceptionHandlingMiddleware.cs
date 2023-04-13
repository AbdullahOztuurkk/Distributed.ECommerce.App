using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Clicco.WebAPI.Middlewares
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
                CustomError customError;
                var response = context.Response;
                switch (error)
                {
                    case CustomException e:
                        customError = e.CustomError;
                        _logger.LogError(customError.ErrorMessage);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        customError = CustomErrors.UnexceptedError;
                        _logger.LogError(error.ToString());
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsJsonAsync(
                    customError
                );
            }
        }
    }
}
