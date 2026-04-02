using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionBase
{
    public abstract class JourneyException : SystemException
    {
        public JourneyException(string message) : base(message)
        { }
        public abstract HttpStatusCode GetStatusCode();
    }
}