
using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.Entities.Dominios;

public class ProfessorDominio
{
    [Key]
    public int ProfessorId { get; set; }
    public string? UserId { get; set; }
    public string? ProfessorNome { get; set; }
    public string? CPF { get; set; }
    public ApplicationUser? User { get; set; }
    public string? Especialidade { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataContratacao { get; set; }

    // Relacionamentos
    public virtual ICollection<DisciplinaDominio> Disciplinas { get; set; } = new List<DisciplinaDominio>();
    public virtual ICollection<TurmaDominio> Turmas { get; set; } = new List<TurmaDominio>();
}