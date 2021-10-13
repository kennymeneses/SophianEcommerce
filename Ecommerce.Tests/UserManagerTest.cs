using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Ecommerce.Models.InputsBody;
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

        [Fact]
        public async Task It_ShouldFindASpecifiedUser()
        {
            var userFound = await userManager.SearchById(2002);

            Assert.True(userFound.apiStatus == "ok");
        }

        [Fact]
        public async Task It_ShouldCreateAnUser()
        {
            var userInput = new UserInput()
            {
                nick= "userRandom",
                namesUser = "nameRandom",
                surnameUser = "surnamesUser",
                numberDocumentUser = "987654321",
                rolIdUser = 1,
                emailUser = "user@youremail.com",
                ageUser = 30,
                genderUser = "male",
                dateCreation = DateTime.Now,
                removed = false,
                passwordUser = "blahblah"                
            };

            var createResponse = await userManager.Create(userInput);

            Assert.True(createResponse.apiStatus == "ok");
        }

        [Fact]
        public async Task It_UpdateAPropertyUser()
        {
            var userInput = new UserInput()
            {
                nick = "userForCrate",
                namesUser = "nameRandom",
                surnameUser = "surnameRandom",
                numberDocumentUser = "123456789",
                rolIdUser = 1,
                emailUser = "random@correo.com",
                ageUser = 30,
                genderUser = "male",
                dateCreation = DateTime.Now,
                removed = false
            };

            var updateResponse = await userManager.Update(2001, userInput);

            Assert.True(updateResponse.apiStatus == "ok");
        }

        [Fact]
        public async Task It_ShouldRemoveAnSpecifiedUser()
        {
            var removeResponse = await userManager.Remove(2001);

            Assert.True(removeResponse.apiStatus == "ok");
        }
    }
}
