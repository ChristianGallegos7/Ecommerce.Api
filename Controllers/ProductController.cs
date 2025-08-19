using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Models;
using Ecommerce.Api.Models.Dtos;
using Ecommerce.Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetProducts", Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts();
            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return Ok(productsDto);
        }


        [HttpGet("GetProduct/{id:int}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound($"El producto con id {id} no fue encontrado.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(productDto);

        }

        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (createProductDto == null)
            {
                return BadRequest("Los datos del producto son inválidos.");
            }

            if (!_productRepository.ProductExists(createProductDto.Name!))
            {
                ModelState.AddModelError("Nombre", "El producto ya existe.");
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExists(createProductDto.CategoryId))
            {
                ModelState.AddModelError("CategoriaId", "La categoría no existe.");
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(createProductDto);

            if (!_productRepository.CreateProduct(product))
            {
                ModelState.AddModelError("", $"Ocurrió un error al guardar el producto {product.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

    }
}