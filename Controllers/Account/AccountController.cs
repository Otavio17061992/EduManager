using EduManager.InfraEstrutura.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EduManager.Models.ViewModels;
using EduManager.Models.Entities.Dominios;
using System.Threading.Tasks;
using EduManager.Models.Entities.Metodos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace EduManager.Controllers.Login
{
    public class AccountController : Controller
    {
        private readonly AccountMetodos _accountMetodos;

        public AccountController(AccountMetodos accountMetodos)
        {
            _accountMetodos = accountMetodos;
        }

        [HttpGet]
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
                return RedirectToAction("Index","Home");
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
                return Json(new {sucess = false, message = "CPF não informado"});
            }


            bool exists = await _accountMetodos.VerifyCpfAsync(request.Cpf);

            if(exists)
            {
                return Json(new {sucess = true, message = "CPF já cadastrado."});
            }

            return Json(new {sucess = false, message = "CPF disponivel ou não encontradao"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasswordByCpf([FromBody] ResetPasswordRequest model)
        {
            if(!ModelState.IsValid)
                return Json(new {sucess = false, message = "Dados inválidos."});

            var result = await _accountMetodos.ResetPasswordByCpf(model.Cpf, model.NewPassword);

            if(result.Succeeded)
            {
                return Json(new {sucess = true, message = "Senha redefinida com sucesso"});
            }

            var error = result.Errors.FirstOrDefault()?.Description ?? "Erro ao redefinir senha.";
            return Json(new { success = false, message = error });
        }
    }
}