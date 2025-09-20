using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime DataMatricula { get; set; }
        public int CursoId { get; set; }
        public virtual Curso? Curso { get; set; }
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
        public virtual ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
        


        // Propriedade calculada para aluno
        public string? NomeCompleto => User?.NomeCompleto;
        public int? Idade => User?.DataNascimento == null ? null : 
            (int)((DateTime.Now - User.DataNascimento).TotalDays / 365.25);
        public double? MediaNotas => Notas != null && Notas.Count > 0 ? 
            Notas.Average(n => n.Valor) : null;
        public string StatusMatricula => Ativo ? "Ativo" : "Inativo";


    }
}