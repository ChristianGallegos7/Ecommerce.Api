using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Api.Data;
using Ecommerce.Api.Models;
using Ecommerce.Api.Models.Dtos;
using Ecommerce.Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Api.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;
        private string? secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }

        public User GetUser(int id)
        {
            return _db.Users.FirstOrDefault(u => u.Id == id)!;
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(u => u.Name).ToList();
        }

        public bool IsUniqueUser(string name)
        {
            return _db.Users.Any(u => u.Name.ToLower().Trim() == name.ToLower().Trim());
        }


        public async Task<User> Register(CreateUserDto createUserDto)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            var user = new User
            {
                Username = createUserDto.Username,
                Name = createUserDto.Name,
                Password = hashPassword,
                Role = createUserDto.Role,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            if (string.IsNullOrWhiteSpace(userLoginDto.Username) || string.IsNullOrWhiteSpace(userLoginDto.Password))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    Message = "El Username y contraseña son requeridos",
                    User = null
                };
            }

            var user = await _db.Users.FirstOrDefaultAsync<User>(u => u.Username.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());

            if (user == null)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    Message = "Username no encontrado",
                    User = null
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    Message = "Credenciales incorrectas",
                    User = null
                };
            }

            var handlerToken = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username!),
                    new Claim(ClaimTypes.Role, user.Role!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handlerToken.CreateToken(tokenDescriptor);

            return new UserLoginResponseDto()
            {
                Token = handlerToken.WriteToken(token),
                User = new UserRegisterDto()
                {
                    Username = user.Username,
                    Name = user.Name,
                    Role = user.Role,
                    Password = user.Password ?? ""
                },
                Message = "Login exitoso"
            };


        }

    }
}