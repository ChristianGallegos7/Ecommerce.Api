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

        public Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }
    }
}