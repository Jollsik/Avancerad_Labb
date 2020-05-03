using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            Product product = new Product();
            product.ID = Guid.NewGuid();
            product.Name = "Glas";
            product.Price = 105.00M;
            product.Description = "Ett stort högkvalitativt drickglas";
            product.imageURL = "https://picsum.photos/200";


            Product product2 = new Product();
            product2.ID = Guid.NewGuid();
            product2.Name = "Lampa";
            product2.Price = 333.33M;
            product2.Description = "En stark led-lampa";
            product2.imageURL = "https://picsum.photos/200";

            products.Add(product);
            products.Add(product2);


            return View(products);
        }
    }
}