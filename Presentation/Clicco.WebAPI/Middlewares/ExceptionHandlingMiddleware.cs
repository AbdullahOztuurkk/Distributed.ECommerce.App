using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Core.ResponseModel;
using System.Net;
using static Clicco.Domain.Core.Exceptions.Errors;

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
                ResponseDto response = new();
                Error customError;
                var httpResponse = context.Response;
                switch (error)
                {
                    case CustomException e:
                        customError = e.CustomError;
                        _logger.LogError(customError.ErrorMessage);
                        httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        customError = Errors.UnexceptedError;
                        _logger.LogError(error.ToString());
                        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await httpResponse.WriteAsJsonAsync(
                    response.Fail(customError)
                );
            }
        }
    }
}
