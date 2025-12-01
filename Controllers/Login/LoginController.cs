using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EduManager.InfraEstrutura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EduManager.Controllers.Login
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly EduManagerContext _EduManagerContext;

        public LoginController(ILogger<LoginController> logger, EduManagerContext eduManagerContext)
        {
            _logger = logger;
            _EduManagerContext = eduManagerContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}