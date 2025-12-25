using EduManager.InfraEstrutura.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EduManager.Models.ViewModels;
using EduManager.Models.Entities.Dominios; 


namespace EduManager.Controllers.Login
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly SignInManager<ApplicationUser> _signInManager; 


        public AccountController(
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) 
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email!);
                
                if (user == null)
                {
                    _logger.LogWarning("Usuário não encontrado.");
                }
                else
                {
                    // var novoHash = _userManager.PasswordHasher.HashPassword(user, model.Password!);
                    // Console.WriteLine($"O hash correto para a senha digitada é: {novoHash}");

                    var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password!, false);
                    Console.WriteLine(passwordCheck);
                    
                    if (!passwordCheck.Succeeded)
                    {
                        // Aqui você pode inspecionar o 'passwordCheck' no Debug (F5)
                        // Veja se 'IsLockedOut' ou 'IsNotAllowed' é true
                        _logger.LogWarning($"Falha: Locked={passwordCheck.IsLockedOut}, NotAllowed={passwordCheck.IsNotAllowed}");
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuário logado com sucesso.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogWarning("Tentativa de login inválida.");
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
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