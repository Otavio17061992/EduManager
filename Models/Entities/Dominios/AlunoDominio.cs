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

        public string? NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataMatricula { get; set; }
        public int CursoId { get; set; }
        public bool Ativo { get; set; } = true;
        public string? CPF { get; set; }
        public string? Email { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual CursoDominio Curso { get; set; } = null!; 
        public virtual ICollection<NotaDominio> Notas { get; set; } = new List<NotaDominio>();
        public virtual ICollection<FrequenciaDominio> Frequencias { get; set; } = new List<FrequenciaDominio>();
    }
}