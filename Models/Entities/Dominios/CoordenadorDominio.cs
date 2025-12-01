using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class CoordenadorDominio
    {
        [Key]
        public int CoordenadorId { get; set; }
        public string? UserId { get; set; }
        public string? CoordenadorNome { get; set;}
        public string? CoordenadorCPF { get; set; }
        public string? CoordenadorEmail { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime CoordenadorDataContratacao { get; set; }
        public decimal CoordenadorSalario { get; set; }

        // Relacionamentos
        public virtual ICollection<CursoDominio> Cursos { get; set; } = new List<CursoDominio>();

        // Propriedade calculada
        public string? NomeCompleto => User?.NomeCompleto;
        public int TempoServicoAnos => (int)((DateTime.Now - CoordenadorDataContratacao).TotalDays / 365.25);
        public string SalarioFormatado => CoordenadorSalario.ToString("C");
        public int QuantidadeCursos => Cursos?.Count ?? 0;
    }
}