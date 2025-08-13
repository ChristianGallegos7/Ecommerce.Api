using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Models;
using Ecommerce.Api.Models.Dtos;

namespace Ecommerce.Api.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}