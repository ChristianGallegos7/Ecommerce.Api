using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Models;
using Ecommerce.Api.Models.Dtos;

namespace Ecommerce.Api.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);

        bool IsUniqueUser(string name);

        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);

        Task<User> Register(CreateUserDto createUserDto);
    }
}