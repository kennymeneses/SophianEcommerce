using Ecommerce.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Tests
{
    public class ToolsTest
    {
        private readonly EncryptData encrypt = new EncryptData();

        [Fact]
        public void GenerateHash()
        {
            //Site Resource for generate string to SHA256 
            //https://passwordsgenerator.net/sha256-hash-generator/

            string password = "PasswordVerySecured!";
            string hashToCompare = "9A15DF759BFC4997A6FE2B2B49E34ED23C317CA09E6684CC95DFF0C0E1C6BD32";

            string Hash = encrypt.PasswordToHash(password);

            Assert.Equal(hashToCompare, Hash);
        }
    }
}
