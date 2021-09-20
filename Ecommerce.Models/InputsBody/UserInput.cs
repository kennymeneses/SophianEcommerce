using System;

namespace Ecommerce.Models.InputsBody
{
    public class UserInput : BaseInputEntity
    {
        public string nick { get; set; }
        public string namesUser { get; set; }
        public string surnameUser { get; set; }
        public string numberDocumentUser { get; set; }
        public int rolIdUser { get; set; }
        public string emailUser { get; set; }
        public string passwordUser { get; set; }
        public int ageUser { get; set; }
        public string genderUser { get; set; }
        public DateTime dateCreation { get; set; }
        public bool removed { get; set; }

        public UserInput()
        {
            nick = string.Empty;
            namesUser = string.Empty;
            surnameUser = string.Empty;
            numberDocumentUser = string.Empty;
            rolIdUser = 0;
            emailUser = string.Empty;
            passwordUser = string.Empty;
            ageUser = 0;
            genderUser = string.Empty;
            dateCreation = DateTime.Now;
            removed = false;
        }
    }
}
