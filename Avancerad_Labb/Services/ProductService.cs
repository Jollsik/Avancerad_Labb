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
            product.ID = new Guid("979e9625-d418-4888-8dfa-a864a3de286d");
            product.Name = "Glas";
            product.Price = 105.00M;
            product.Description = "Ett stort högkvalitativt drickglas";
            product.imageURL = "https://picsum.photos/100";
            Products.Add(product);


            Product product2 = new Product();
            product2.ID = new Guid("76be19c6-79a0-4dff-93bc-d474098b59e3");
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

        public Product GetProductById(Guid Id)
        {
            foreach(var product in Products)
            {
                if (product.ID == Id)
                {
                    return product;
                }
            }
            return null;
        }
    }
}
