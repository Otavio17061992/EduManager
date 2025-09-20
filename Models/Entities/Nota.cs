using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities
{
    public class Nota
    {
        public int NotaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public double Valor { get; set; }
        public DateTime DataLancamento { get; set; }

        // Relacionamentos
        public virtual Aluno? Aluno { get; set; }
        public virtual Disciplina? Disciplina { get; set; }
        
        // propriedades Calculadas
        public string? Status => Valor >= 60 ? "Aprovado" : "Reprovado";
        public string ValorFormatado => Valor.ToString("0.00");


    }
}