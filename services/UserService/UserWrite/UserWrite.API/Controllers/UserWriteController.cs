using GlobalShared.Lib.BaseResponseModels;
using Microsoft.AspNetCore.Mvc;
using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;
using UserWrite.API.Repository.Interfaces;

namespace UserWrite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWriteController : ControllerBase
    {
        private readonly IUserWriteRepository _userWriteRepository;

        public UserWriteController(IUserWriteRepository userWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<User>>> Register([FromBody] RegisterUserDto userRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new IResponseModel<User> { Error = true, Message = "Invalid registration data." });
                }

                var user = await _userWriteRepository.RegisterUserAsync(userRegistration);

                return CreatedAtAction(nameof(Register), new IResponseModel<User>
                {
                    Data = user,
                    Error = false,
                    Message = "User registered successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<User>
                {
                    Error = true,
                    Message = $"Registration failed: {ex.Message}"
                });
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(IResponseModel<UserLoginResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<UserLoginResultDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<UserLoginResultDto>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<UserLoginResultDto>>> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new IResponseModel<UserLoginResultDto> { Error = true, Message = "Invalid login data." });
                }

                var result = await _userWriteRepository.LoginAsync(loginDto);

                return Ok(new IResponseModel<UserLoginResultDto>
                {
                    Data = result,
                    Error = false,
                    Message = "Login successful."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<UserLoginResultDto>
                {
                    Error = true,
                    Message = $"Login failed: {ex.Message}"
                });
            }
        }
    }
}
