using GlobalShared.Lib.BaseResponseModels;
using Microsoft.AspNetCore.Mvc;
using UserRead.API.Repository.Interfaces;
using UserShared.Lib.Models;

namespace UserRead.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileReadController : ControllerBase
    {
        private readonly IProfileReadRepository _profileReadRepository;

        public ProfileReadController(IProfileReadRepository profileReadRepository)
        {
            _profileReadRepository = profileReadRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 200)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 400)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 404)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 500)]
        public async Task<ActionResult<IResponseModel<Profile>>> GetProfileById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest(new IResponseModel<Profile>
                    {
                        Error = true,
                        Message = "Invalid profile ID."
                    });
                }

                var profile = await _profileReadRepository.GetProfileByIdAsync(id);

                if (profile == null)
                {
                    return NotFound(new IResponseModel<Profile>
                    {
                        Error = true,
                        Message = "Profile not found."
                    });
                }

                return Ok(new IResponseModel<Profile>
                {
                    Data = profile,
                    Error = false,
                    Message = "Profile retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<Profile>
                {
                    Error = true,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpGet("by-user/{userId}")]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 200)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 400)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 404)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), 500)]
        public async Task<ActionResult<IResponseModel<Profile>>> GetProfileByUserId(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest(new IResponseModel<Profile>
                    {
                        Error = true,
                        Message = "Invalid user ID."
                    });
                }

                var profile = await _profileReadRepository.GetProfileByUserIdAsync(userId);

                if (profile == null)
                {
                    return NotFound(new IResponseModel<Profile>
                    {
                        Error = true,
                        Message = "Profile not found."
                    });
                }

                return Ok(new IResponseModel<Profile>
                {
                    Data = profile,
                    Error = false,
                    Message = "Profile retrieved successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<Profile>
                {
                    Error = true,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<Profile>>), 200)]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<Profile>>), 400)]
        [ProducesResponseType(typeof(IResponseModel<IEnumerable<Profile>>), 500)]
        public async Task<ActionResult<IResponseModel<IEnumerable<Profile>>>> GetAllProfiles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    return BadRequest(new IResponseModel<IEnumerable<Profile>>
                    {
                        Error = true,
                        Message = "Invalid pagination values."
                    });
                }

                var profiles = await _profileReadRepository.GetAllProfilesAsync(pageNumber, pageSize);

                return Ok(new IResponseModel<IEnumerable<Profile>>
                {
                    Data = profiles,
                    Error = false,
                    Message = "Profiles fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<IEnumerable<Profile>>
                {
                    Error = true,
                    Message = $"An error occurred: {ex.Message}"
                });
            }
        }
    }
}
