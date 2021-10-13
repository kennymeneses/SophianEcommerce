using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Ecommerce.Tools;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Tests
{
    public class UserManagerTest
    {
        private readonly DataContext context = DataContextInitializer.GetContext();
        private readonly Mock<ILogger<UserManager>> mock_logger = new Mock<ILogger<UserManager>>();
        private readonly EncryptData encrypt = new EncryptData();

        UserManager userManager => new UserManager(
            context,
            mock_logger.Object,
            encrypt);

        public UserManagerTest()
        {
            context.users.Add(new User()
            {
                userId = 2001,
                nickUser = "userRandom",
                namesUser = "nameRandom",
                surnamesUser = "surnameRandom",
                dniUser = "123456789",
                roleId = 1,
                emailUser = "random@correo.net",
                ageUser = 30,
                genderUser = "male",
                dataCreated = DateTime.Now,
                removed = false
            });

            context.users.Add(new User()
            {
                userId = 2002,
                nickUser = "SomeoneUser",
                namesUser = "SomeName",
                surnamesUser = "SomeSurname",
                dniUser = "023456789",
                roleId = 2,
                emailUser = "some@correo.net",
                ageUser = 25,
                genderUser = "male",
                dataCreated = DateTime.Now,
                removed = false
            });

            context.users.Add(new User()
            {
                userId = 2003,
                nickUser = "clientNick",
                namesUser = "clientName",
                surnamesUser = "clientSurname",
                dniUser = "323456789",
                roleId = 3,
                emailUser = "client@correo.net",
                ageUser = 27,
                genderUser = "male",
                dataCreated = DateTime.Now,
                removed = false
            });

            context.SaveChanges();
        }

        [Fact]
        public async Task It_ShouldReturnsUserList()
        {
            var responseUserSearch = await userManager.Search(0, 3);

            Assert.Equal(3, responseUserSearch.data.Count);
        }
    }
}
