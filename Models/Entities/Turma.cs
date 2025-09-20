using System;
using System.Collections.Generic;

namespace EduManager.Models.Entities
{
    public class Turma
    {
        public int TurmaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativa { get; set; } = true;

        // Relacionamentos
        public virtual Curso? Curso { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public virtual ICollection<Professor> Professores { get; set; } = new List<Professor>();

        // Propriedades calculadas
        public int DuracaoDias => (DataFim - DataInicio).Days;
        public int QuantidadeAlunos => Alunos?.Count ?? 0;
        public string Status => Ativa ? "Ativa" : "Inativa";
    }
}