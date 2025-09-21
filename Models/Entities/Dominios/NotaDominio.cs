using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class NotaDominio
    {
        [Key]
        public int NotaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public double Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataAvaliacao { get; set; }
        public string? TipoAvaliacao { get; set; }
        public int TurmaId { get; set; }

        // Relacionamentos
        public virtual AlunoDominio? Aluno { get; set; }
        public virtual DisciplinaDominio? Disciplina { get; set; }
        
        // propriedades Calculadas
        public string? Status => Valor >= 60 ? "Aprovado" : "Reprovado";
        public string ValorFormatado => Valor.ToString("0.00");


    }
}