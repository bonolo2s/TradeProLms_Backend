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
        public async Task<ActionResult<IResponseModel<User>>> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new IResponseModel<User>
                {
                    Error = true,
                    Message = "Invalid user ID."
                });
            }

            var user = await _userReadRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new IResponseModel<User>
                {
                    Error = true,
                    Message = "User not found."
                });
            }

            return Ok(new IResponseModel<User>
            {
                Data = user,
                Error = false,
                Message = "User retrieved successfully."
            });
        }

        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<IResponseModel<User>>> GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new IResponseModel<User>
                {
                    Error = true,
                    Message = "Username is required."
                });
            }

            var user = await _userReadRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound(new IResponseModel<User>
                {
                    Error = true,
                    Message = "User not found."
                });
            }

            return Ok(new IResponseModel<User>
            {
                Data = user,
                Error = false,
                Message = "User found."
            });
        }

        [HttpGet("all")]
        public async Task<ActionResult<IResponseModel<IEnumerable<User>>>> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new IResponseModel<IEnumerable<User>>
                {
                    Error = true,
                    Message = "Invalid pagination values."
                });
            }

            var users = await _userReadRepository.GetAllUsersAsync(pageNumber, pageSize);

            return Ok(new IResponseModel<IEnumerable<User>>
            {
                Data = users,
                Error = false,
                Message = "Users fetched successfully."
            });
        }

        [HttpGet("exists")]
        public async Task<ActionResult<IResponseModel<bool>>> UserExists([FromQuery] string username, [FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new IResponseModel<bool>
                {
                    Error = true,
                    Message = "Username and email are required."
                });
            }

            var exists = await _userReadRepository.UserExistsAsync(username, email);

            return Ok(new IResponseModel<bool>
            {
                Data = exists,
                Error = false,
                Message = exists ? "User exists." : "User does not exist."
            });
        }
    }
}
