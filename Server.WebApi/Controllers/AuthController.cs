using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Business.Abstract;
using Server.DataAccess.Abstract;
using Shared.Lib.DTOs;
using Shared.Lib.Entities;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;


        public AuthController(IUserService userService)
        {
            _userService = userService;
   
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterDto user) {

            try
            {
                var response = await _userService.CreateUser(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {

                var response = await _userService.SignIn(user);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(AppRefreshToken refresh)
        {
            try
            {

                var response = await _userService.RefreshToken(refresh);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
