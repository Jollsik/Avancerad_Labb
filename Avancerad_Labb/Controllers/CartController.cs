using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.Models;
using Avancerad_Labb.Services;
using Avancerad_Labb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");

            CartViewModel cvm = new CartViewModel();
            if(cart.Value != null)
            {
                string[] split = cart.Value.Split(",");
                cvm.TotalPrice = 100;
                cvm.products = new List<Tuple<int, Product>>();
                foreach (var stringId in split)
                {
                    int amount = 0;
                    for(int i = 0; i < split.Length; i++)
                    {
                        if(stringId == split[i])
                        {
                            amount++;
                        }
                    }
                    var product = _productService.GetProductById(new Guid(stringId));

                    if(product != null)
                    {
                        int a = 0;
                        foreach(var tuple in cvm.products)
                        {
                            if(tuple.Item2.ID == product.ID)
                            {
                                a++;
                            }
                        }
                        if(a == 0)
                        {
                            var productTuple = Tuple.Create(amount, product);
                            cvm.products.Add(productTuple);
                        }
                    }

                }
            }
            return View(cvm);

        }
        public IActionResult RemoveItem(string id, CartViewModel cvm)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");

            if (cart.Value != null)
            {
                string[] split = cart.Value.Split(",");
                cvm.TotalPrice = 100;
                //cvm.products = new List<string>();
                for (int i = 0; i < split.Length; i++)
                {
                    if(split[i] == id)
                    {
                        split[i] = "";
                    }
                }
            }

                return RedirectToAction("Index", "Cart");
        }
    }
}