using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EduManager.InfraEstrutura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EduManager.Models;
using EduManager.Models.ViewModels;

namespace EduManager.Controllers.Login
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly EduManagerContext _EduManagerContext;

        public AccountController(ILogger<AccountController> logger, EduManagerContext eduManagerContext)
        {
            _logger = logger;
            _EduManagerContext = eduManagerContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool loginSuccessful = true;

                if (loginSuccessful)
                {
                    
                    return RedirectToAction("Index","Home");
                }
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}