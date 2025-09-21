using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.Entities.Dominios
{
    public class TurmaDominio
    {
        [Key]
        public int TurmaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int CursoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativa { get; set; } = true;

        // Relacionamentos
        public virtual CursoDominio? Curso { get; set; }
        public virtual ICollection<AlunoDominio> Alunos { get; set; } = new List<AlunoDominio>();
        public virtual ICollection<DisciplinaDominio> Disciplinas { get; set; } = new List<DisciplinaDominio>();
        public virtual ICollection<ProfessorDominio> Professores { get; set; } = new List<ProfessorDominio>();

        // Propriedades calculadas
        public int DuracaoDias => (DataFim - DataInicio).Days;
        public int QuantidadeAlunos => Alunos?.Count ?? 0;
        public string Status => Ativa ? "Ativa" : "Inativa";
    }
}