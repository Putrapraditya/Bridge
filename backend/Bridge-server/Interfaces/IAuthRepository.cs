using Bridge_server.DTOs.Auth;


namespace Bridge_server.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
