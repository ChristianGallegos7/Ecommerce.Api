using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Models.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "El nombre de la categoria es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre de la categoria no puede exceder los 50 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre de la categoria no puede ser menor de 3 caracteres")]

        public string? Name { get; set; }
    }
}