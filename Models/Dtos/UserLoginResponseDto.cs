using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Models.Dtos
{
    public class UserLoginResponseDto
    {
        public string? Token { get; set; }

        public UserRegisterDto? User { get; set; }

        public string? Message { get; set; }

    }
}