using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avancerad_Labb.Product.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProductDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            Models.Product[] products = new Models.Product[]
            {
                new Models.Product(){Name="Glas", Description="Ett stort högkvalitativt drickglas",Price=105.00M, imageURL="https://picsum.photos/500"},
                new Models.Product(){Name="Lampa", Description="En stark led-lampa",Price=333.33M, imageURL="https://picsum.photos/500"},
                new Models.Product(){Name="Solglasögon", Description="Svarta pilot-glasögon",Price=500M, imageURL="https://picsum.photos/500"},
                new Models.Product(){Name="Jeans", Description="Blåa herrjeans",Price=600M, imageURL="https://picsum.photos/500"},
                new Models.Product(){Name="Skrivbord", Description="Ett ljust, stadigt skrivbord med mycket utrymme",Price=1399.99M, imageURL="https://picsum.photos/500"},
                new Models.Product(){Name="Klocka", Description="En av bara 20 exemplar av märket Rolex",Price=160000M, imageURL="https://picsum.photos/500"}
            /*product1.ID = new Guid("979e9625-d418-4888-8dfa-a864a3de286d");

            product2.ID = new Guid("76be19c6-79a0-4dff-93bc-d474098b59e3");

            product3.ID = new Guid("fb33465a-97e7-4f27-80d9-38c980f20025");

            product4.ID = new Guid("4c76972a-59d1-435e-888c-367b52782904");

            product5.ID = new Guid("0207ed1f-d604-4f6b-8624-952f15398bae");

            product6.ID = new Guid("5d0840ec-d086-4403-8af6-f2776e17bd0c");*/
            };
            foreach(var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
