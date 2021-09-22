using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.Models.InputsBody;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Ecommerce.ServiceApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        ILogger<UsersController> _logger;
        IUserManager _manager;

        public UsersController(IUserManager manager, ILogger<UsersController> logger)
        {
            _logger = logger;
            _manager = manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(int page = 0, int size = 3)
        {
            try
            {
                var query = _manager.Search(page, size);

                if (query.Result.apiStatus.Equals("error"))
                {
                    return NotFound();
                }
                
                return Ok(query.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return NotFound(ex.Message);
            }
        }

        //[AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {               
                var singleQuery = _manager.SearchById(id);

                if (singleQuery.Result.apiStatus.Equals("error"))
                {
                    return NotFound(singleQuery.Result);
                }

                return Ok(singleQuery.Result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] UserInput newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var checkStatus = _manager.Create(newUser);

                if (checkStatus.Result.apiStatus != "ok")
                {
                    return StatusCode(503, checkStatus.Result);
                }

                var code = Int32.Parse(checkStatus.Result.code);

                return StatusCode(code, checkStatus.Result);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error in Post method: {0} ", ex.Message);

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserInput modifyUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var checkStatus = _manager.Update(id, modifyUser);

                if (checkStatus.Result.apiStatus.Equals("error"))
                {
                    return StatusCode(422, checkStatus.Result);
                }

                return Ok(checkStatus.Result);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error in Update method: {0} ", ex.Message);

                throw;
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var checkStatus = _manager.Remove(id);

                if(checkStatus.Result.apiStatus.Equals("error"))
                {
                    return StatusCode(404, checkStatus.Result);
                }

                return Ok(checkStatus.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Delete method: {0} ", ex.Message);

                throw;
            }
        }
    }
}
