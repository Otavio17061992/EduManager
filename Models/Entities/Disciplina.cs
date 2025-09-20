namespace EduManager.Models.Entities;

public class Disciplina
{
    public int DisciplinaId { get; set; }
    public string? Nome { get; set; }
    public int CargaHoraria { get; set; }
    public int? ProfessorId { get; set; }
    public virtual Professor? Professor { get; set; }

    // Relacionamentos
    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}