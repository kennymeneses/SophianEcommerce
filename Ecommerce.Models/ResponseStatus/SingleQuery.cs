using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.ResponseStatus
{
    public class SingleQuery
    {
        public string apiStatus { get; set; }
        public string apiMessage { get; set; }
        public object entity { get; set; }

    }
}
