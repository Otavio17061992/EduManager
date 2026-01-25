using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Interfaces;
using EduManager.Models.Entities.Dominios;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EduManager.Models.ViewModels;


namespace EduManager.Models.Entities.Metodos
{
    public class AccountMetodos : IAccountRepository
    {
        private readonly UserManager<ApplicationUser>? _userManager;
        private readonly SignInManager<ApplicationUser>? _signInManager;


        public AccountMetodos(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure:false);
        }

        public async Task<bool> SignOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> VerifyCpfAsync(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            var cleanCpf = cpf.Replace(".", "").Replace("-", "");

            return await _userManager.Users.AnyAsync(u => u.CPF == cleanCpf);
        }

        public async Task<IdentityResult> ResetPasswordByCpf(string cpf, string newPassword)
        {
            if (string.IsNullOrEmpty(cpf))
                return IdentityResult.Failed(new IdentityError { Description = "CPF não informado." });

            var cleanCpf = new string(cpf.Where(char.IsDigit).ToArray());
            
            // Busca o usuário via UserManager
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.CPF == cleanCpf);
            
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado com este CPF." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}