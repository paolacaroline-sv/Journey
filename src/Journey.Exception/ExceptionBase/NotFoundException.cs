using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionBase
{
    public class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrors()
        {
            return new List<string> { Message };
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}