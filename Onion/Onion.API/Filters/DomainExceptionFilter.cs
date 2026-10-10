using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Onion.Domain.Exceptions;

namespace Onion.API.Filters;

// Turns a DomainException into a 400 instead of the 500 the framework would
// return for an unhandled exception.
public class DomainExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not DomainException exception)
        {
            return;
        }

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Invalid request",
            Detail = exception.Message,
        };

        context.Result = new BadRequestObjectResult(problem);
        context.ExceptionHandled = true;
    }
}
