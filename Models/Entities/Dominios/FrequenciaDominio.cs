using System;
using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.Entities.Dominios
{
    public class FrequenciaDominio
    {
        public int FrequenciaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public DateTime DataAula { get; set; } 
        public bool Presente { get; set; }
        public int TurmaId { get; set; }

        public virtual AlunoDominio Aluno { get; set; } = null!;
        public virtual DisciplinaDominio Disciplina { get; set; } = null!;
        public virtual TurmaDominio Turma { get; set; } = null!;
    }
}
