using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journey.Communication.Requests
{
    public class RequestRegisterActivityJson
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}