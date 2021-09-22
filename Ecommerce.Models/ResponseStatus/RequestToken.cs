using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.ResponseStatus
{
    public class RequestToken
    {
        public string responseStatus { get; set; }
        public string responseMessagge { get; set; }
        public object entity { get; set; }

        public RequestToken()
        {
            responseStatus = string.Empty;
            responseMessagge = string.Empty;
            entity = null;
        }
    }
}
