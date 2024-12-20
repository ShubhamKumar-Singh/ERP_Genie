using ERP_Genie_Service.Contract.Interfaces;
using ERP_Genie_Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ERP_Genie_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login user method
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var response = await _authService.Login(loginModel);
            return response.IsSuccess ? Ok(response) : Unauthorized(response);
        }

        /// <summary>
        /// Registering the user records
        /// </summary>
        /// <param name="signupModel"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignupModel signupModel)
        {
            var response = await _authService.Register(signupModel);
            return response.IsSuccess ? Ok(response) : Conflict(response);
        }

        /// <summary>
        /// Logout the user
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.Logout();
            return Ok(response);
        }
    }
}
