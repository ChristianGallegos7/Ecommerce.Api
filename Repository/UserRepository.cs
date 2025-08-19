using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Data;
using Ecommerce.Api.Models;
using Ecommerce.Api.Models.Dtos;
using Ecommerce.Api.Repository.IRepository;

namespace Ecommerce.Api.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;

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
        // Asegúrate de instalar el paquete BCrypt.Net-Next:
        // 

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

        public Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

    }
}