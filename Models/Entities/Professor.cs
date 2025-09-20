
namespace EduManager.Models.Entities;

public class Professor
{
    public int ProfessorId { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string? Especialidade { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataContratacao { get; set; }

    // Relacionamentos
    public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}