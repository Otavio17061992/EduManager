using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities
{
    public class Curso
    {
        public int CursoId { get; set; }
        public string? NomeCurso { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public bool Ativo { get; set; } = true;

        // Relacionamentos
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();

        // Propriedade calculada
        public string Status => Ativo ? "Ativo" : "Inativo";
        public int DuracaoDias => (DataTermino - DataInicio).Days;
        public int QuantidadeAlunos => Alunos?.Count ?? 0;
    }
}