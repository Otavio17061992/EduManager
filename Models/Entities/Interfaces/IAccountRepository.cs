using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;
using EduManager.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EduManager.Models.Entities.Interfaces
{
    public interface IAccountRepository
    {
        Task <bool>SignOutAsync();
        Task<SignInResult> Login(LoginViewModel model);
        Task<bool> VerifyCpfAsync(string cpf);
        Task<IdentityResult> ResetPasswordByCpf(string cpf, string newPassword);

    }
}