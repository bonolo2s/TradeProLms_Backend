using GlobalShared.Lib.BaseResponseModels;
using Microsoft.AspNetCore.Mvc;
using UserRead.API.Repository.Interfaces;
using UserShared.Lib.Models;

namespace UserRead.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReadController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;

        public UserReadController(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<User>>> GetUserById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(new IResponseModel<User> { Error = true, Message = "Invalid user ID." });
                }

                var user = await _userReadRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound(new IResponseModel<User> { Error = true, Message = "User not found." });
                }

                return Ok(new IResponseModel<User> { Data = user, Error = false, Message = "User retrieved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<User> { Error = true, Message = $"Server error: {ex.Message}" });
            }
        }

        [HttpGet("by-username/{username}")]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<User>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<User>>> GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    return BadRequest(new IResponseModel<User> { Error = true, Message = "Username is required." });
                }

                var user = await _userReadRepository.GetUserByUsernameAsync(username);

                if (user == null)
                {
                    return NotFound(new IResponseModel<User> { Error = true, Message = "User not found." });
                }

                return Ok(new IResponseModel<User> { Data = user, Error = false, Message = "User found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<User> { Error = true, Message = $"Server error: {ex.Message}" });
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<User>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<User>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<User>>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<IEnumerable<User>>>> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest(new IResponseModel<IEnumerable<User>> { Error = true, Message = "Invalid pagination values." });
                }

                var users = await _userReadRepository.GetAllUsersAsync(pageNumber, pageSize);

                return Ok(new IResponseModel<IEnumerable<User>> { Data = users, Error = false, Message = "Users fetched successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<IEnumerable<User>> { Error = true, Message = $"Server error: {ex.Message}" });
            }
        }

        [HttpGet("exists")]
        [ProducesResponseType(typeof(IResponseModel<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<bool>>> UserExists([FromQuery] string username, [FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest(new IResponseModel<bool> { Error = true, Message = "Username and email are required." });
                }

                var exists = await _userReadRepository.UserExistsAsync(username, email);

                return Ok(new IResponseModel<bool> { Data = exists, Error = false, Message = exists ? "User exists." : "User does not exist." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<bool> { Error = true, Message = $"Server error: {ex.Message}" });
            }
        }
    }
}
