using System.Security.Claims;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Dominios;
using EduManager.Models.Entities.Interfaces;
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
        private readonly IAlunosRepository _alunosRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(EduManagerContext context, IAlunosRepository alunosRepository, ITurmaRepository turmaRepository, IProfessorRepository professorRepository)
        {
            _context = context;
            _alunosRepository = alunosRepository;
            _turmaRepository = turmaRepository;
            _professorRepository = professorRepository;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var professor = await _professorRepository.ListarProfessorPorUserId(userId);

            if (professor == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var totalAlunos = await _alunosRepository.ContarAlunosPorProfessorAsync(professor.ProfessorId);

            var viewModel = new ProfessorDashboardViewModel
            {
                ProfessorNome = professor.ProfessorNome,
                Especialidade = professor.Especialidade,
                TotalTurmas = professor.Turmas.Count(t => t.Ativa),
                TotalDisciplinas = professor.Disciplinas.Count,
                TotalAlunos = totalAlunos,
                AvaliacoesPendentes = 0,
                TurmasAtivas = professor.Turmas.Where(t => t.Ativa).ToList(),
                MinhasDisciplinas = professor.Disciplinas.ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> MinhasTurmas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var professor = await _professorRepository.ListarProfessorPorUserId(userId);

            if (professor == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var turmas = await _turmaRepository.ListarTurmasPorProfessorAsync(professor.ProfessorId);

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

        [HttpGet]
        public async Task<IActionResult> LancarNotas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var professor = await _professorRepository.ListarProfessorPorUserId(userId);
            if (professor == null) return RedirectToAction("AccessDenied", "Account");

            var turmas = await _turmaRepository.ListarTurmasPorProfessorAsync(professor.ProfessorId);
            ViewBag.Turmas = turmas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LancarNotas(LancarNotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nota = new NotaDominio
                {
                    AlunoId = model.AlunoId,
                    DisciplinaId = model.DisciplinaId,
                    Valor = model.Valor,
                    TipoAvaliacao = model.TipoAvaliacao,
                    DataAvaliacao = model.DataAvaliacao
                };

                await _professorRepository.LancarNotaAsync(nota);
                TempData["MensagemSucesso"] = "Nota lançada com sucesso!";
                return RedirectToAction(nameof(LancarNotas));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var professor = await _professorRepository.ListarProfessorPorUserId(userId);
            if (professor != null)
            {
                ViewBag.Turmas = await _turmaRepository.ListarTurmasPorProfessorAsync(professor.ProfessorId);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarFrequencia()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var professor = await _professorRepository.ListarProfessorPorUserId(userId);
            if (professor == null) return RedirectToAction("AccessDenied", "Account");

            var turmas = await _turmaRepository.ListarTurmasPorProfessorAsync(professor.ProfessorId);
            ViewBag.Turmas = turmas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarFrequencia(List<FrequenciaDominio> frequencias)
        {
            if (frequencias != null && frequencias.Any())
            {
                await _professorRepository.RegistrarFrequenciaAsync(frequencias);
                TempData["MensagemSucesso"] = "Frequência registrada com sucesso!";
                return RedirectToAction(nameof(RegistrarFrequencia));
            }
            // Handle error or empty list
            return RedirectToAction(nameof(RegistrarFrequencia));
        }

        [HttpGet]
        public async Task<IActionResult> GetAlunosPorTurma(int turmaId)
        {
            var alunos = await _professorRepository.ListarAlunosPorTurmaAsync(turmaId);
            return Json(alunos.Select(a => new { a.AlunoId, a.AlunoNomeCompleto }));
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