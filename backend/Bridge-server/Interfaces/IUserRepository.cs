using Bridge_server.Entities;
using Bridge_server.DTOs;
using Bridge_server.Entities;

namespace Bridge_server.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(Guid id);
        Task<MsUsersC> CreateUserAsync(UserCreateDto userDto, Guid creatorId);
        Task<bool> UpdateUserAsync(Guid id, UserUpdateDto userDto, Guid updaterId);
        Task<bool> DeleteUserAsync(Guid id, Guid deleterId);
    }
}
