using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.ResponseStatus
{
    public class DataQuery
    {
        public List<object> data { get; set; }
        public string apiStatus { get; set; }
        public string apiMessage { get; set; }
        public int total { get; set; }

        public DataQuery()
        {
            data = new List<object>();
            apiStatus = "ok";
            apiMessage = string.Empty;
            total = 0;
        }
    }
}
