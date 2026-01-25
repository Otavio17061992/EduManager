using EduManager.Models.Entities.Dominios;

namespace EduManager.Models.ViewModels
{
    public class ProfessorDashboardViewModel
    {
        public string? ProfessorNome { get; set; }
        public string? Especialidade { get; set; }

        // Estatísticas
        public int TotalTurmas { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalAlunos { get; set; }
        public int AvaliacoesPendentes { get; set; }

        // Listas
        public List<TurmaDominio> TurmasAtivas { get; set; } = new List<TurmaDominio>();
        public List<DisciplinaDominio> MinhasDisciplinas { get; set; } = new List<DisciplinaDominio>();
        public List<ProximaAulaViewModel> ProximasAulas { get; set; } = new List<ProximaAulaViewModel>();
    }

    public class ProximaAulaViewModel
    {
        public string? NomeDisciplina { get; set; }
        public string? NomeTurma { get; set; }
        public DateTime DataHora { get; set; }
        public string? Sala { get; set; }
    }
}
