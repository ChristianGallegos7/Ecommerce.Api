using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Models;

namespace Ecommerce.Api.Repository.IRepository
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        ICollection<Product> GetProductsForCategory(int categoryId);

        ICollection<Product> SearchProduct(string name);

        Product GetProduct(int id);

        bool BuyProduct(string name, int quantity);

        bool ProductExists(int id);

        bool ProductExists(string name);

        bool CreateProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(Product product);

        bool Save();
    }
}