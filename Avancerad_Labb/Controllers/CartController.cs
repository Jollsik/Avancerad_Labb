using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avancerad_Labb.Models;
using Avancerad_Labb.Services;
using Avancerad_Labb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avancerad_Labb.Controllers
{
    public class CartController : AppController
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");

            CartViewModel cvm = new CartViewModel();
            cvm.TotalPrice = 0;
            if (cart.Value != null && cart.Value.Length > 0)
            {
                string[] split = cart.Value.Split(",");
                cvm.OrderRows = new List<OrderRow>();
                foreach (var stringId in split)
                {
                    //Check for amount
                    int amount = 0;
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (stringId == split[i])
                        {
                            amount++;
                        }
                    }
                    var product = _productService.GetProductById(new Guid(stringId));
                    cvm.TotalPrice += product.Result.Price;
                    if (product != null)
                    {
                        //Don't create duplicates
                        int exists = 0;
                        foreach (var item in cvm.OrderRows)
                        {
                            if (item.Product.ID == product.Result.ID)
                            {
                                exists++;
                            }
                        }
                        if (exists == 0)
                        {
                            OrderRow orderRow = new OrderRow { Product = product.Result, Amount = amount };
                            cvm.OrderRows.Add(orderRow);
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
                    if (split[i] == id)
                    {
                        split[i] = "";
                    }
                }
                string cartContent = "";
                foreach (var item in split)
                {
                    if (item.Length > 0)
                    {
                        cartContent += item + ",";
                    }
                }
                if (cartContent.Length > 0)
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
        public IActionResult MinusOneAmount(string id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            List<string> spltlist = new List<string>(cart.Value.Split(","));
            spltlist.Remove(id);

            string cartContent = "";
            foreach (var item in spltlist)
            {
                if (!string.IsNullOrEmpty(cartContent))
                {
                    cartContent += "," + item;
                }
                else
                {
                    cartContent += item;
                }
            }
            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult PlusOneAmount(string id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = cart.Value;
            cartContent += "," + id;

            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public IActionResult PlaceOrder([Bind("TotalPrice, OrderRows")]CartViewModel vm)
        {
            Order order = new Order();
            order.Id = Guid.NewGuid();
            order.TotalPrice = vm.TotalPrice;
            order.Date = DateTime.Now;
            order.UserId = Guid.Parse(_userManager.GetUserId(User));
            order.OrderRows = vm.OrderRows;

            return RedirectToAction("OrderSuccess", new { id = order.Id });
        }
        public IActionResult OrderSuccess(Guid id)
        {

            return View();
        }
    }
}