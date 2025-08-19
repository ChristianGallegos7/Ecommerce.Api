using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Models.Dtos
{
    public class CreateProductDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImgUrl { get; set; }

        public string? SKU { get; set; }
        public int Stock { get; set; }

        public DateTime UpdateDate { get; set; }

        public int CategoryId { get; set; }
    }
}