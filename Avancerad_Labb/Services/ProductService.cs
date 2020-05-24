using Avancerad_Labb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Avancerad_Labb.Services
{
    public class ProductService : IProductService
    {
        public List<Product> ProductList = new List<Product>();
        public ProductService()
        {
            Product product1 = new Product();
            product1.ID = new Guid("979e9625-d418-4888-8dfa-a864a3de286d");
            product1.Name = "Glas";
            product1.Price = 105.00M;
            product1.Description = "Ett stort högkvalitativt drickglas";
            product1.imageURL = "https://picsum.photos/500";
            ProductList.Add(product1);


            Product product2 = new Product();
            product2.ID = new Guid("76be19c6-79a0-4dff-93bc-d474098b59e3");
            product2.Name = "Lampa";
            product2.Price = 333.33M;
            product2.Description = "En stark led-lampa";
            product2.imageURL = "https://picsum.photos/500";
            ProductList.Add(product2);

            Product product3 = new Product();
            product3.ID = new Guid("fb33465a-97e7-4f27-80d9-38c980f20025");
            product3.Name = "Solglasögon";
            product3.Price = 500M;
            product3.Description = "Svarta pilot-glasögon";
            product3.imageURL = "https://picsum.photos/500";
            ProductList.Add(product3);

            Product product4 = new Product();
            product4.ID = new Guid("4c76972a-59d1-435e-888c-367b52782904");
            product4.Name = "Jeans";
            product4.Price = 600M;
            product4.Description = "Blåa herrjeans";
            product4.imageURL = "https://picsum.photos/500";
            ProductList.Add(product4);

            Product product5 = new Product();
            product5.ID = new Guid("0207ed1f-d604-4f6b-8624-952f15398bae");
            product5.Name = "Skrivbord";
            product5.Price = 1399.99M;
            product5.Description = "Ett ljust, stadigt skrivbord med mycket utrymme";
            product5.imageURL = "https://picsum.photos/500";
            ProductList.Add(product5);

            Product product6 = new Product();
            product6.ID = new Guid("5d0840ec-d086-4403-8af6-f2776e17bd0c");
            product6.Name = "Klocka";
            product6.Price = 160000M ;
            product6.Description = "En av bara 20 exemplar av märket Rolex";
            product6.imageURL = "https://picsum.photos/500";
            ProductList.Add(product6);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return ProductList;
        }
        public Product GetProductById(Guid Id)
        {
            foreach(var product in ProductList)
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
