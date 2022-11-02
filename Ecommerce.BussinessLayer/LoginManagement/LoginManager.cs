using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Ecommerce.Models.InputsBody;
using Ecommerce.Models.Outputs;
using Ecommerce.Models.ResponseStatus;
using Ecommerce.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BussinessLayer.LoginManagement
{
    public class LoginManager : ILoginManager
    {
        private readonly IConfiguration _configuration;
        DataContext _context;
        ILogger<LoginManager> _logger;
        DefaultValues _defaultValues;
        EncryptData _encrypt;
        
        public LoginManager(DataContext context, 
                            ILogger<LoginManager> logger, 
                            IConfiguration configuration,
                            EncryptData encrypt,
                            DefaultValues defaultValues)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _encrypt = encrypt;
            _defaultValues = defaultValues;
        }

        public OutputToken BuildToken(LoginInput input, User user)
        {
            var output = new OutputToken();

            _logger.LogInformation("The creation token started.");

            var _claims = new[]
            {
                new Claim(ClaimTypes.Email, input.email),
                new Claim("IdUser", user.userId.ToString()),
                new Claim(ClaimTypes.Role, _defaultValues.roles[user.roleId])
            };
            var palabra = _configuration["key_secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key_secret"]));
            var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "SophianCustomer",
                audience: "SophianEcommerce",
                claims: _claims,
                expires: expiration,
                signingCredentials: signCredential
                );

            var tokn = new JwtSecurityTokenHandler().WriteToken(token);

            output.token = tokn;
            output.expirationTime = expiration;
            output.idUser = user.userId;

            return output;
        }

        public async Task<RequestToken> ValidateLogin(LoginInput input)
        {
            var responseRequest = new RequestToken();
            var user = new User();
            var userPass = new UserPass();
            var tokenBuilded = new OutputToken();
            string passInput = string.Empty;
            DateTime localDate = DateTime.Now;

            try
            {
                var timer = Stopwatch.StartNew();

                _logger.LogInformation("A validation login for email {0} started at: {1}",input.email, localDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                user = await _context.users.FirstOrDefaultAsync(u => u.emailUser == input.email);
                
                if(user == null)
                {
                    responseRequest.responseMessagge = String.Format("This email doesnt associate for a user account.");
                    responseRequest.responseStatus = "error";
                    responseRequest.entity = null;

                    _logger.LogError("The validation login fails for the email: {0}", input.email);

                    return responseRequest;
                }

                userPass = await _context.userPass.FirstOrDefaultAsync(up => up.userId == user.userId);
                passInput = _encrypt.PasswordToHash(input.password);
                
                if(userPass.passwordHash != passInput)
                {
                    _logger.LogError("The validation login fails because the password is wrong.");

                    responseRequest.responseMessagge = String.Format("The validation login fails because the password is wrong.");
                    responseRequest.responseStatus = "error";
                    responseRequest.entity = null;

                    return responseRequest;
                }

                tokenBuilded = BuildToken(input, user);

                timer.Stop();

                _logger.LogInformation("The validation was succesfully and took: {0} ms.", timer.ElapsedMilliseconds);

                responseRequest.responseMessagge = String.Format("The validation was succesfully.");
                responseRequest.responseStatus = "ok";
                responseRequest.entity = tokenBuilded;

                return responseRequest;
            }
            catch (Exception ex)
            {
                responseRequest.responseStatus = "error";
                responseRequest.responseMessagge = String.Format("{0} for more details : {1}", ex.Message, ex.InnerException);
                responseRequest.entity = null;

                return responseRequest;
            }
        }
    }
}
