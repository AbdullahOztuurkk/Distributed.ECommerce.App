﻿using Clicco.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clicco.WebAPI.Filters
{
    public class RequestValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// It puts the incoming request into the 
        // validation rules before it enters the controller, and if it is not verified, 
        /// it cancels and prints the errors on the screen.
        /// </summary>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(new ErrorResponse<BaseEntity>(errors[0]));
                return;
            }
            await next();
        }
    }
}
