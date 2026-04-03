using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Communication.Responses;
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
                
                var responseJson = new ResponseErrorsJson(journeyException.GetErrors());
                context.Result = new ObjectResult(responseJson);
                return;
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500;

                var responseJson = new ResponseErrorsJson(new List<string> { "An unexpected error occurred." });               
                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}