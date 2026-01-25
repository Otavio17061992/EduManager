using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;

namespace EduManager.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAlunos { get; set; }
        public int TotalProfessores { get; set; }
        public int TotalCursos { get; set; }
        public int TotalTurmas { get; set; }
        public List<AlunoDominio> AlunosRecentes { get; set; } = new List<AlunoDominio>();
    }
}