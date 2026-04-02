using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionBase
{
    public class ErrorOnValidationException : JourneyException
    {
        public ErrorOnValidationException(string message) : base(message)
        {
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}