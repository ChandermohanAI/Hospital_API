using Hospital_API.Model.DTO.LoginRegister;
using Hospital_API.Repository;
using Hospital_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_API.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

		//dependency injection for Userrepository

		public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


		// user Post EndPoint To Ensure a User Exists
		// Endpoint With LoginRequest_DTO type Input 
		[HttpPost("Login")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Login([FromBody] LoginRequest_DTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.Users == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                return BadRequest(new { message = "Username or Password is Incorrect" });
            }
            return Ok(loginResponse);
        }

		// user Post EndPoint To Create User
		// Endpoint With RegisterationRequest_DTO type Input 
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool isUnique = _userRepository.IsUniqueUser(model.UserName);
            if (!isUnique)
            {
                ModelState.AddModelError("Custom Error", "Username Already Exist");
                return BadRequest();
            }
            var client = await _userRepository.Register(model);
            if(client == null)
            {
                ModelState.AddModelError("Custom Error", "Empty Input");
                return BadRequest();
            }
            return Ok(client);
        }

    }
}
