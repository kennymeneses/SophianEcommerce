using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.Models.InputsBody;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ecommerce.ServiceApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        ILogger<UsersController> _logger;
        IUserManager _manager;

        public UsersController(IUserManager manager, ILogger<UsersController> logger)
        {
            _logger = logger;
            _manager = manager;
        }

        //[Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Get(int page = 0, int size = 3)
        {
            try
            {
                var query = await _manager.Search(page, size);

                if (query.apiStatus.Equals("error"))
                {
                    return NotFound();
                }
                
                return Ok(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return NotFound(ex.Message);
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {               
                var singleQuery = await _manager.SearchById(id);

                if (singleQuery.apiStatus.Equals("error"))
                {
                    return NotFound(singleQuery);
                }

                return Ok(singleQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInput newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var checkStatus = await _manager.Create(newUser);

                if (checkStatus.apiStatus != "ok")
                {
                    return StatusCode(503, checkStatus);
                }

                var code = Int32.Parse(checkStatus.code);

                return StatusCode(code, checkStatus);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error in Post method: {0} ", ex.Message);

                throw;
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserInput modifyUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var checkStatus = await _manager.Update(id, modifyUser);

                if (checkStatus.apiStatus.Equals("error"))
                {
                    return StatusCode(422, checkStatus);
                }

                return Ok(checkStatus);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error in Update method: {0} ", ex.Message);

                throw;
            }
        }

        //[Authorize(Roles = "SuperAdmin, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var checkStatus = await _manager.Remove(id);

                if(checkStatus.apiStatus.Equals("error"))
                {
                    return StatusCode(404, checkStatus);
                }

                return Ok(checkStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Delete method: {0} ", ex.Message);

                throw;
            }
        }
    }
}
