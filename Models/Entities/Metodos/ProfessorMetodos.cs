using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Dominios;
using EduManager.Models.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EduManager.Models.Entities.Metodos
{
    public class ProfessorMetodos : IProfessorRepository
    {
        private readonly EduManagerContext _context;

        public ProfessorMetodos(EduManagerContext context)
        {
            _context = context;
        }

        public async Task<ProfessorDominio?> ListarProfessorPorId(int professorId)
        {
            return await _context.Professor
                .Include(p => p.User)
                .Include(p => p.Disciplinas)
                .Include(p => p.Turmas)
                .FirstOrDefaultAsync(p => p.ProfessorId == professorId);
        }

        public async Task<ProfessorDominio?> ListarProfessorPorUserId(string userId)
        {
            return await _context.Professor
                .Include(p => p.User)
                .Include(p => p.Disciplinas)
                .Include(p => p.Turmas)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task LancarNotaAsync(NotaDominio nota)
        {
            _context.Nota.Add(nota);
            await _context.SaveChangesAsync();
        }

        public async Task RegistrarFrequenciaAsync(List<FrequenciaDominio> frequencias)
        {
            _context.Frequencia.AddRange(frequencias);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AlunoDominio>> ListarAlunosPorTurmaAsync(int turmaId)
        {
            var turma = await _context.Turma.FindAsync(turmaId);
            if (turma == null) return Enumerable.Empty<AlunoDominio>();

            return await _context.Aluno
                .Include(a => a.User)
                .Where(a => a.CursoId == turma.CursoId)
                .ToListAsync();
        }
    }
}