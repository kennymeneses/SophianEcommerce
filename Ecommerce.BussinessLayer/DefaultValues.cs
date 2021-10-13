using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Ecommerce.BussinessLayer
{
    public class DefaultValues
    {
        public Dictionary<int, string> roles { get; set; }
        public Dictionary<string, string> config { get; set; }

        public DefaultValues()
        {
            roles = new Dictionary<int, string>()
            {
                {1, "SuperAdmin" },
                {2, "Admin" },
                {3, "Client" },
                {4, "Visitor" }
            };

            config = new Dictionary<string, string>()
            {
                {"key_secret", "L14v3Mv1S3c374@!$&/"}
            };
        }
    }
}
