using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class NotaDominio
    {
        public int NotaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public string? TipoAvaliacao { get; set; }

        public virtual AlunoDominio Aluno { get; set; } = null!;
        public virtual DisciplinaDominio Disciplina { get; set; } = null!;
    }

}