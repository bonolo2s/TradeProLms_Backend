using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserShared.Lib.ReqModels;
using UserWrite.API.Repository.Interfaces;
using UserShared.Lib.Models;
using GlobalShared.Lib.BaseResponseModels;
using Microsoft.AspNetCore.Authorization;

namespace UserWrite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileWriteController : ControllerBase
    {
        private readonly IProfileWriteRepository _profileWriteRepository;

        public ProfileWriteController(IProfileWriteRepository profileWriteRepository)
        {
            _profileWriteRepository = profileWriteRepository;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<Profile>>> CreateProfile([FromBody] ProfileCreateDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new IResponseModel<Profile>
                {
                    Error = true,
                    Message = "Invalid profile data.",
                    Data = null
                });

            try
            {
                var createdProfile = await _profileWriteRepository.CreateProfileAsync(profileDto);
                return CreatedAtAction(nameof(CreateProfile), new IResponseModel<Profile>
                {
                    Error = false,
                    Message = "Profile created successfully.",
                    Data = createdProfile
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new IResponseModel<Profile>
                {
                    Error = true,
                    Message = $"Error creating profile: {ex.Message}"
                });
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IResponseModel<Profile>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<Profile>>> UpdateProfile([FromBody] ProfileUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new IResponseModel<Profile>
                {
                    Error = true,
                    Message = "Invalid profile update data."
                });

            try
            {
                var updatedProfile = await _profileWriteRepository.UpdateProfileAsync(updateDto);
                return Ok(new IResponseModel<Profile>
                {
                    Error = false,
                    Message = "Profile updated successfully.",
                    Data = updatedProfile
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                    return NotFound(new IResponseModel<Profile>
                    {
                        Error = true,
                        Message = "Profile not found."
                    });

                return StatusCode(500, new IResponseModel<Profile>
                {
                    Error = true,
                    Message = $"Error updating profile: {ex.Message}"
                });
            }
        }

        [HttpDelete("delete/{userId}")]
        [ProducesResponseType(typeof(IResponseModel<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IResponseModel<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IResponseModel<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IResponseModel<object>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IResponseModel<object>>> DeleteProfile(Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest(new IResponseModel<object>
                {
                    Error = true,
                    Message = "Invalid user ID."
                });

            try
            {
                await _profileWriteRepository.DeleteProfileAsync(userId);
                return Ok(new IResponseModel<object>
                {
                    Error = false,
                    Message = "Profile deleted successfully."
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                    return NotFound(new IResponseModel<object>
                    {
                        Error = true,
                        Message = "Profile not found."
                    });

                return StatusCode(500, new IResponseModel<object>
                {
                    Error = true,
                    Message = $"Error deleting profile: {ex.Message}"
                });
            }
        }
    }
}
