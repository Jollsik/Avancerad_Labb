using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            CartViewModel cvm = new CartViewModel();

            string[] split = cart.Value.Split(",");
            cvm.TotalPrice = 100;
            cvm.products = new List<string>();
            foreach (var item in split)
            {
                cvm.products.Add(item);
            }
            return View(cvm);

        }
    }
}