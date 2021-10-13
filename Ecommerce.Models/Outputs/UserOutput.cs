using System;

namespace Ecommerce.Models.Outputs
{
    public class UserOutput
    {
        public int id { get; set; }

        public string nick { get; set; }

        public string names { get; set; }

        public string surnames { get; set; }

        public string dni { get; set; }

        public int roleId { get; set; }

        public string email { get; set; }

        public int age { get; set; }

        public string gender { get; set; }

        public string dataCreated { get; set; }
    }
}
