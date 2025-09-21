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
        public int DisciplinaId { get; set; }
        public string? Ano { get; set; }
        public int Semestre { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public bool Ativa { get; set; } = true;
        public int ProfessorId { get; set; }

        public virtual CursoDominio Curso { get; set; } = null!;
        public virtual DisciplinaDominio Disciplina { get; set; } = null!;
        public virtual ProfessorDominio Professor { get; set; } = null!;
        public virtual ICollection<FrequenciaDominio> Frequencias { get; set; } = new List<FrequenciaDominio>();

        // Propriedades calculadas
        public int DuracaoDias => (DataFim - DataInicio).Days;
        public string Status => Ativa ? "Ativa" : "Inativa";
    }
}
