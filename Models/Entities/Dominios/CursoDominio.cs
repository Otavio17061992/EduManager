using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class CursoDominio
    {
        [Key]
        public int CursoId { get; set; }
        public string? NomeCurso { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int CargaHoraria { get; set; }
        public bool Ativo { get; set; } = true;
        public int CoordenadorId { get; set; }

        public virtual CoordenadorDominio Coordenador { get; set; } = null!;
        public virtual ICollection<AlunoDominio> Alunos { get; set; } = new List<AlunoDominio>();
        public virtual ICollection<DisciplinaDominio> Disciplinas { get; set; } = new List<DisciplinaDominio>();

        // Propriedade calculada
        public string Status => Ativo ? "Ativo" : "Inativo";
        public int DuracaoDias => (DataTermino - DataInicio).Days;
        public int QuantidadeAlunos => Alunos?.Count ?? 0;
    }
}
