using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeyException = (JourneyException)context.Exception;
                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
                context.Result = new ObjectResult(context.Exception.Message);
                return;
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = new ObjectResult("An unexpected error occurred.");
            }
        }
    }
}