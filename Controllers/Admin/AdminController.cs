using System.Diagnostics;
using EduManager.Models;
using EduManager.InfraEstrutura.Security;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.ViewModels;
using EduManager.Models.Entities.Metodos;
using Microsoft.AspNetCore.Mvc;

namespace EduManager.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly EduManagerContext _context;
    private readonly AlunoMetodos _alunoMetodos;

    public AdminController(ILogger<AdminController> logger, EduManagerContext context, AlunoMetodos alunoMetodos)
    {
        _logger = logger;
        _context = context;
        _alunoMetodos = alunoMetodos;
    }

    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            TotalAlunos = _context.Aluno.Count(),
            TotalProfessores = _context.Professor.Count(),
            TotalCursos = _context.Curso.Count(),
            TotalTurmas = _context.Turma.Count(),
            AlunosRecentes = _context.Aluno.OrderByDescending(a => a.AlunoDataMatricula).Take(5).ToList()
        };
        return View(model);
    }

    public IActionResult ConfiguracoesSistema()
    {
        return View();
    }

    public IActionResult GerenciarPermissoes()
    {
        // buscar usuários e roles
        return View();
    }

    public async Task<IActionResult> VerAluno(int id)
    {
        var aluno = await _alunoMetodos.ObterPorIdAsync(id);
        if (aluno == null)
        {
            return NotFound();
        }
        return View(aluno);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
