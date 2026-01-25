using EduManager.InfraEstrutura.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EduManager.Models.ViewModels;
using EduManager.Models.Entities.Dominios;
using System.Threading.Tasks;
using EduManager.Models.Entities.Metodos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;


namespace EduManager.Controllers.Login
{
    [Authorize(Roles = "AdminMaster")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AccountMetodos _accountMetodos;
        private readonly UserManager<ApplicationUser> _userManager; 

        public AccountController(AccountMetodos accountMetodos, UserManager<ApplicationUser> userManager)
        {
            _accountMetodos = accountMetodos;
            _userManager = userManager; 
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountMetodos.SignOutAsync();

            if (result)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Error", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _accountMetodos.Login(model);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                    
                if (await _userManager.IsInRoleAsync(user, "Master"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (await _userManager.IsInRoleAsync(user, "Professor"))
                {
                    return RedirectToAction("Dashboard", "Professor");
                }
                else if (await _userManager.IsInRoleAsync(user, "Aluno"))
                {
                    return RedirectToAction("Home", "Aluno");
                }

                return RedirectToAction("Index", "Home");
            }

            if(result.IsLockedOut)
            {
                ModelState.AddModelError("","Esta conta está temporariamente bloqueada.");
                return View(model);
            }

            if(result.RequiresTwoFactor)
            {
                return RedirectToAction("LoginWith2fa");
            }

            ModelState.TryAddModelError(string.Empty,"E-mail ou senha inválidos.");
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCpf([FromBody] CpfRequest request)
        {
            if(request == null || string.IsNullOrEmpty(request.Cpf))
            {
                return Json(new { success = false, message = "CPF não informado" });
            }

            var cleanCpf = request.Cpf.Replace(".", "").Replace("-", "");
            bool exists = await _accountMetodos.VerifyCpfAsync(cleanCpf);

            if(exists)
            {
                return Json(new { success = true, message = "Usuário encontrado! Defina sua nova senha." });
            }

            return Json(new { success = false, message = "CPF não encontrado em nossa base." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordByCpf([FromBody] ResetPasswordRequest model)
        {
            if(!ModelState.IsValid)
                return Json(new { success = false, message = "Dados inválidos." }); 

            var result = await _accountMetodos.ResetPasswordByCpf(model.Cpf, model.NewPassword);

            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Senha redefinida com sucesso" });
            }

            var error = result.Errors.FirstOrDefault()?.Description ?? "Erro ao redefinir senha.";
            return Json(new { success = false, message = error });
        }
    }
}