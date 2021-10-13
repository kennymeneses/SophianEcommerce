using Ecommerce.BussinessLayer;
using Ecommerce.BussinessLayer.LoginManagement;
using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Ecommerce.Models.InputsBody;
using Ecommerce.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Ecommerce.Tests
{
    public class LoginManagerTest : IClassFixture<ConfigTest>
    {
        private readonly DataContext context = DataContextInitializer.GetContext();
        private readonly Mock<ILogger<LoginManager>> mock_logger = new Mock<ILogger<LoginManager>>();
        EncryptData _encrypt = new EncryptData();        
        DefaultValues _defaultValues = new DefaultValues();
        private ServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public LoginManagerTest(ConfigTest configTest)
        {
            _serviceProvider = configTest.ServiceProvider;
            _configuration = _serviceProvider.GetService<IConfiguration>();
        }

        LoginManager loginManager => new LoginManager(
            context, 
            mock_logger.Object, 
            _configuration, 
            _encrypt,
            _defaultValues);

        [Fact]
        public void It_ShoulRetunsToken()
        {
            var user = new User()
            {
                userId = 2010,
                nickUser = "RandomUser",
                namesUser = "RandomName",
                surnamesUser = "SurnameUser",
                dniUser = "12345678",
                roleId = 1,
                emailUser = "random@correo.net",
                ageUser = 30,
                genderUser = "male",
                dataCreated = DateTime.Now,
                removed = false
            };

            var loginInput = new LoginInput()
            {
                email = "random@correo.net",
                password = "contrasenarandom"
            };

            var tokenBuilded = loginManager.BuildToken(loginInput, user);

            Assert.NotNull(tokenBuilded.token);
        }
    }
}
