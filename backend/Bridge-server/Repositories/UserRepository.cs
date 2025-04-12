using Microsoft.EntityFrameworkCore;
using Bridge_server.Data;
using Bridge_server.Entities;
using Bridge_server.DTOs;
using Bridge_server.Helpers;
using Bridge_server.Interfaces;
using Bridge_server.Entities;

namespace Bridge_server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            return await _context.MsUsersC
                .Select(u => new UserReadDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    isActive = u.isActive
                }).ToListAsync();
        }

        public async Task<UserReadDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.MsUsersC.FindAsync(id);
            if (user == null) return null;

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                isActive = user.isActive
            };
        }

        public async Task<MsUsersC> CreateUserAsync(UserCreateDto dto, Guid creatorId)
        {
            var user = new MsUsersC
            {
                Name = dto.Name,
                Username = dto.Username,
                Password = PasswordHasher.HashPassword(dto.Password),
                Email = dto.Email,
                Phonenumber = dto.Phonenumber,
                Role = dto.Role,
                isActive = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorId
            };

            _context.MsUsersC.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserUpdateDto dto, Guid updaterId)
        {
            var user = await _context.MsUsersC.FindAsync(id);
            if (user == null) return false;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Phonenumber = dto.Phonenumber;
            user.Role = dto.Role;
            user.isActive = dto.isActive;
            user.UpdatedAt = DateTime.UtcNow;
            user.UpdatedBy = updaterId;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid id, Guid deleterId)
        {
            var user = await _context.MsUsersC.FindAsync(id);
            if (user == null) return false;

            user.isActive = 0;


            return await _context.SaveChangesAsync() > 0;
        }
    }
}
