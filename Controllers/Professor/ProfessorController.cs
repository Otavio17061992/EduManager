using System.Security.Claims;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Controllers.Professor
{
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private readonly EduManagerContext _context;

        public ProfessorController(EduManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var professor = await _context.Professor
                .Include(p => p.User)
                .Include(p => p.Disciplinas)
                .Include(p => p.Turmas)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (professor == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            // Buscar alunos únicos das turmas do professor
            var alunosIds = await _context.Turma
                .Where(t => t.ProfessorId == professor.ProfessorId)
                .SelectMany(t => t.Frequencias.Select(f => f.AlunoId))
                .Distinct()
                .CountAsync();

            var viewModel = new ProfessorDashboardViewModel
            {
                ProfessorNome = professor.ProfessorNome,
                Especialidade = professor.Especialidade,
                TotalTurmas = professor.Turmas.Count(t => t.Ativa),
                TotalDisciplinas = professor.Disciplinas.Count,
                TotalAlunos = alunosIds,
                AvaliacoesPendentes = 0, // Pode implementar lógica de avaliações pendentes
                TurmasAtivas = professor.Turmas.Where(t => t.Ativa).ToList(),
                MinhasDisciplinas = professor.Disciplinas.ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> MinhasTurmas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var turmas = await _context.Turma
                .Include(t => t.Disciplina)
                .Include(t => t.Curso)
                .Include(t => t.Frequencias)
                .Where(t => t.Professor.UserId == userId)
                .ToListAsync();

            return View(turmas);
        }

        public async Task<IActionResult> MinhasDisciplinas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var disciplinas = await _context.Disciplina
                .Include(d => d.Curso)
                .Include(d => d.Turmas)
                .Where(d => d.Professor.UserId == userId)
                .ToListAsync();

            return View(disciplinas);
        }

        public IActionResult LancarNotas()
        {
            return View();
        }

        public IActionResult RegistrarFrequencia()
        {
            return View();
        }

        public async Task<IActionResult> MeusAlunos()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var alunos = await _context.Aluno
                .Include(a => a.Curso)
                .Where(a => a.Curso.Disciplinas.Any(d => d.Professor.UserId == userId))
                .Distinct()
                .ToListAsync();

            return View(alunos);
        }
    }
}