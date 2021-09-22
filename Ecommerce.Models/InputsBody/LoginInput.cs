using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.InputsBody
{
    public class LoginInput
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginInput()
        {
            email = string.Empty;
            password = string.Empty;
        }
    }
}
