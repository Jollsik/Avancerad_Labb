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
using static Avancerad_Labb.ViewModels.CartViewModel;

namespace Avancerad_Labb.Controllers
{
    public class CartController : AppController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartController(IProductService productService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _orderService = orderService;
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
                cvm.OrderProducts = new List<OrderProduct>();
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
                        foreach (var item in cvm.OrderProducts)
                        {
                            if (item.Product.ID == product.Result.ID)
                            {
                                exists++;
                            }
                        }
                        if (exists == 0)
                        {
                            OrderProduct orderProduct = new OrderProduct { Product = product.Result, Amount = amount };
                            cvm.OrderProducts.Add(orderProduct);
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
        public async Task<IActionResult> PlaceOrder([Bind("TotalPrice, OrderProducts")]CartViewModel vm)
        {
            ApplicationUser user = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result;

            Order order = new Order();
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.ZipCode = user.PostalCode;
            order.City = user.City;
            order.Adress = user.StreetAddress;
            order.TotalPrice = vm.TotalPrice;
            order.Date = DateTime.Now;
            order.OrderRows = new List<OrderRow>();
            foreach(var item in vm.OrderProducts)
            {
                OrderRow or = new OrderRow { Name = item.Product.Name, Amount = item.Amount, Price = item.Product.Price };
                order.OrderRows.Add(or);
            }
            var postedOrder = await _orderService.PostOrder(order);
            Response.Cookies.Delete("cart");
            return RedirectToAction("OrderSuccess", new { id = postedOrder.Id });
        }
        public async Task<IActionResult> OrderSuccess(Guid id)
        {
            Order order = await _orderService.GetOrderById(id);
            return View(order);
        }
    }
}