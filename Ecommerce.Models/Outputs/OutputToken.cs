using System;

namespace Ecommerce.Models.Outputs
{
    public class OutputToken
    {
        public string token { get; set; }
        public DateTime expirationTime { get; set; }
        public int idUser { get; set; }
    
        public OutputToken()
        {
            token = string.Empty;
            expirationTime = new DateTime();
            idUser = 0;
        }
    }
}
