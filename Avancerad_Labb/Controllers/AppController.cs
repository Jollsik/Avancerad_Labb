﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Avancerad_Labb.Controllers
{
    public class AppController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            if(cart.Value != null)
            {
                string[] split = cart.Value.Split(",");
                ViewBag.CartCount = split.Count();
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            base.OnActionExecuting(filterContext);

        }
    }
}