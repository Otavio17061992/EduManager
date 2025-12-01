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
        public string? CursoNome { get; set; }
        public string? CursoDescricao { get; set; }
        public DateTime CursoDataInicio { get; set; }
        public DateTime CursoDataTermino { get; set; }
        public int CursoCargaHoraria { get; set; }
        public bool CursoAtivo { get; set; } = true;
        public int CoordenadorId { get; set; }

        public virtual CoordenadorDominio Coordenador { get; set; } = null!;
        public virtual ICollection<AlunoDominio> Alunos { get; set; } = new List<AlunoDominio>();
        public virtual ICollection<DisciplinaDominio> Disciplinas { get; set; } = new List<DisciplinaDominio>();

        // Propriedade calculada
        public string Status => CursoAtivo ? "Ativo" : "Inativo";
        public int DuracaoDias => (CursoDataTermino - CursoDataInicio).Days;
        public int QuantidadeAlunos => Alunos?.Count ?? 0;
    }
}
