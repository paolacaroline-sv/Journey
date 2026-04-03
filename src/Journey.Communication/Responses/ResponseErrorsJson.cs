using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journey.Communication.Responses
{
    public class ResponseErrorsJson
    {
        public IList<string> Errors { get; set; } = [];

        public ResponseErrorsJson(IList<string> errors)
        {
            Errors = errors;
        }
    }
}