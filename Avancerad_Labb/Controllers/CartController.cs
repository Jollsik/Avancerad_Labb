﻿using System;
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
            cvm.TotalPrice = 0;
            if(cart.Value != null && cart.Value.Length > 0)
            {
                string[] split = cart.Value.Split(",");
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
                    cvm.TotalPrice += product.Price;
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
        public IActionResult RemoveItem(string id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");

            if (cart.Value != null && cart.Value.Length > 0)
            {
                string[] split = cart.Value.Split(",");
                for (int i = 0; i < split.Length; i++)
                {
                    if(split[i] == id)
                    {
                        split[i] = "";
                    }
                }
                string cartContent = "";
                foreach(var item in split)
                {
                    if(item.Length > 0)
                    {
                        cartContent += item + ",";
                    }
                }
                if(cartContent.Length > 0)
                {
                    cartContent = cartContent.Remove(cartContent.Length - 1);
                    Response.Cookies.Append("cart", cartContent);
                }
                else
                {
                    Response.Cookies.Delete("cart");
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}