using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.Entities.Dominios;

public class DisciplinaDominio
{
    [Key]
    public int DisciplinaId { get; set; }
    public string? Nome { get; set; }
    public int CargaHoraria { get; set; }
    public int ProfessorId { get; set; }
    public string? Codigo { get; set; }
    public int CursoId { get; set; }

    public virtual ProfessorDominio Professor { get; set; } = null!;
    public virtual CursoDominio Curso { get; set; } = null!;
    public virtual ICollection<TurmaDominio> Turmas { get; set; } = new List<TurmaDominio>();
    public virtual ICollection<NotaDominio> Notas { get; set; } = new List<NotaDominio>();
    public virtual ICollection<FrequenciaDominio> Frequencias { get; set; } = new List<FrequenciaDominio>();
}
