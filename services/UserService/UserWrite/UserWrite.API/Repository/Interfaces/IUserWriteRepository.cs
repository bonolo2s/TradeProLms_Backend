using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;


namespace UserWrite.API.Repository.Interfaces
{
    public interface IUserWriteRepository
    {
        Task RegisterUserAsync(RegisterUserDto userRegistration);
        Task<UserLoginResultDto> LoginAsync(UserLoginDto login);
        Task<UpdateUserResponseDto> UpdateUserAsync();
        Task DeleteUserAsync(Guid userId);
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(PasswordResetDto reset);
    }
}
