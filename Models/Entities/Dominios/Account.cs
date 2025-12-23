using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class Account
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}