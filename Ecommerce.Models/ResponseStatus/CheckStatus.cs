using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.ResponseStatus
{
    public class CheckStatus
    {
        public int id { get; set; }
        public string code { get; set; }
        public string apiStatus { get; set; }
        public string apiMessage { get; set; }

        public CheckStatus()
        {
            id = 0;
            code = string.Empty;
            apiStatus = string.Empty;
            apiMessage = string.Empty;
        }

        public CheckStatus(string apiStatus, string apiMessage)
        {
            id = 0;
            code = string.Empty;
            this.apiStatus = apiStatus;
            this.apiMessage = apiMessage;
        }

        public CheckStatus(int id, string code, string apiStatus, string apiMessage)
        {
            this.id = id;
            this.code = code;
            this.apiStatus = apiStatus;
            this.apiMessage = apiMessage;
        }

    }
}
