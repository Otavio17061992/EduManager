using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class AlunoDominio
    {
        [Key]
        public int AlunoId { get; set; }
        public string? UserId { get; set; }

        public string? AlunoNomeCompleto { get; set; }
        public DateTime AlunoDataNascimento { get; set; }
        public DateTime AlunoDataMatricula { get; set; }
        public int CursoId { get; set; }
        public bool AlunoAtivo { get; set; } = true;
        public string? AlunoCPF { get; set; }
        public string? AlunoEmail { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual CursoDominio Curso { get; set; } = null!; 
        public virtual ICollection<NotaDominio> Notas { get; set; } = new List<NotaDominio>();
        public virtual ICollection<FrequenciaDominio> Frequencias { get; set; } = new List<FrequenciaDominio>();
    }
}