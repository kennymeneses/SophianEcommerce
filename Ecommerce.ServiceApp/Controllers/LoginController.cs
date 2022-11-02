using Ecommerce.BussinessLayer.LoginManagement;
using Ecommerce.Models.InputsBody;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.ServiceApp.Controllers
{
    // it's just for create authorization tokens.
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ILoginManager _manager;

        public LoginController(ILoginManager manager)
        {
            _manager = manager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginInput input)
        {
            var request = await _manager.ValidateLogin(input);

            try
            {
                if(!request.responseStatus.Equals("ok"))
                {
                    return BadRequest(request);
                }

                return Ok(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
