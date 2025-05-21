using UserShared.Lib.ReqModels;
using UserShared.Lib.ResModels;
using UserWrite.API.Repository.Interfaces;

namespace UserWrite.API.Repository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginResultDto> LoginAsync(UserLoginDto login)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(RegisterUserDto userRegistration)
        {
            throw new NotImplementedException();
        }

        public Task RequestPasswordResetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(PasswordResetDto reset)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponseDto> UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
