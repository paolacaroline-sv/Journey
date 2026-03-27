using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionBase
{
    public class JourneyException : SystemException
    {
        public JourneyException(string message) : base(message)
        { }
    }
}