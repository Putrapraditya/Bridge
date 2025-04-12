using Bridge_server.Data;
using Bridge_server.DTOs.Auth;
using Bridge_server.Helpers;
using Bridge_server.Interfaces;
using Bridge_server.DTOs.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bridge_server.Repositories
{
    public class AuthRepositorie : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepositorie(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // 1. Cari user berdasarkan username (jangan cek password di query)
            var user = await _context.MsUsersC
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.isActive == 1);

            // 2. Jika user tidak ditemukan, atau password salah → return null
            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
                return null;

            // 3. Buat JWT token kalau login sukses
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                UserId = user.Id,
                Role = user.Role ?? "User"
            };
        }

    }
}
