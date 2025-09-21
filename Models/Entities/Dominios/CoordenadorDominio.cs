using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduManager.Models.Entities.Dominios
{
    public class CoordenadorDominio
    {
        public int CoordenadorId { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal Salario { get; set; }

        // Relacionamentos
        public virtual ICollection<CursoDominio> Cursos { get; set; } = new List<CursoDominio>();

        // Propriedade calculada
        public string? NomeCompleto => User?.NomeCompleto;
        public int TempoServicoAnos => (int)((DateTime.Now - DataContratacao).TotalDays / 365.25);
        public string SalarioFormatado => Salario.ToString("C");
        public int QuantidadeCursos => Cursos?.Count ?? 0;
    }
}