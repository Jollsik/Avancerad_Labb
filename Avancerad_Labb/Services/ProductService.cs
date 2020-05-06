using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Avancerad_Labb.Services
{
    public class ProductService : IProductService
    {
        public List<Product> Products = new List<Product>();
        public ProductService()
        {
            Product product = new Product();
            product.ID = Guid.NewGuid();
            product.Name = "Glas";
            product.Price = 105.00M;
            product.Description = "Ett stort högkvalitativt drickglas";
            product.imageURL = "https://picsum.photos/100";
            Products.Add(product);


            Product product2 = new Product();
            product2.ID = Guid.NewGuid();
            product2.Name = "Lampa";
            product2.Price = 333.33M;
            product2.Description = "En stark led-lampa";
            product2.imageURL = "https://picsum.photos/100";
            Products.Add(product2);
        }

        public IEnumerable<Product> GetAllProducts()
        {

            return Products;
        }
    }
}
