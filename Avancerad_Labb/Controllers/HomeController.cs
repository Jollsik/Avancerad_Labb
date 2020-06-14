using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Avancerad_Labb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace Avancerad_Labb.Controllers
{
    public class HomeController : AppController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> DeleteUser()
        {
            ApplicationUser user = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result;
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
