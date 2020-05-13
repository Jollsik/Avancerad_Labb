﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.Models;
using Avancerad_Labb.Services;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        public IActionResult Index()
        {
            return View(_productService.GetAllProducts());
        }
        [HttpPost]
        public IActionResult AddToCart(Guid id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = "";

            if(!string.IsNullOrEmpty(cart.Value))
            {
                cartContent = cart.Value;
                cartContent += "," + id;
            }
            else
            {
                cartContent += id;
            }
            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("Index");
        }
    }
}