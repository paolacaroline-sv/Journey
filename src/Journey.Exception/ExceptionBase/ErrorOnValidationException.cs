using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionBase
{
    public class ErrorOnValidationException : JourneyException
    {
        private readonly IList<string> _errors;
        public ErrorOnValidationException(IList<string> message) : base(string.Empty)
        {
            _errors = message;
        }

        public override IList<string> GetErrors()
        {
            return _errors;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}