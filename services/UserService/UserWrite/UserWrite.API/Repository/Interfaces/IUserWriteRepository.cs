using UserShared.Lib.Models;
using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;


namespace UserWrite.API.Repository.Interfaces
{
    public interface IUserWriteRepository
    {
        Task<User> RegisterUserAsync(RegisterUserDto userRegistration);
        Task<UserLoginResultDto> LoginAsync(UserLoginDto login);
        Task<UpdateUserResponseDto> UpdateUserAsync();
        Task<object> DeleteUserAsync(Guid userId);
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(PasswordResetDto reset);
    }
}
