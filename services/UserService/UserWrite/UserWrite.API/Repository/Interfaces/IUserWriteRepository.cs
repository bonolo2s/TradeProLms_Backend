namespace UserWrite.API.Repository.Interfaces
{
    public interface IUserWriteRepository
    {
        Task RegisterUserAsync(UserRegistrationDto userRegistration);
        Task<UserLoginResultDto> LoginAsync(UserLoginDto login);
        Task UpdateUserAsync(Guid userId, UserUpdateDto update);
        Task DeleteUserAsync(Guid userId);
        Task RequestPasswordResetAsync(string email);
        Task ResetPasswordAsync(PasswordResetDto reset);
    }
}
