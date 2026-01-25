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
    public class TurmaMetodos : ITurmaRepository
    {
        private readonly EduManagerContext _context;

        public TurmaMetodos(EduManagerContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(TurmaDominio turma)
        {
            await _context.Turma.AddAsync(turma);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TurmaDominio turma)
        {
            _context.Turma.Update(turma);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var turma = await ObterPorIdAsync(id);
            if (turma != null)
            {
                _context.Turma.Remove(turma);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TurmaDominio?> ObterPorIdAsync(int id)
        {
            return await _context.Turma
                .Include(t => t.Disciplina)
                .Include(t => t.Curso)
                .FirstOrDefaultAsync(t => t.TurmaId == id);
        }

        public async Task<IEnumerable<TurmaDominio>> ListarAsync()
        {
            return await _context.Turma.ToListAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Turma.AnyAsync(t => t.TurmaId == id);
        }

        public async Task<int> ContarTurmasPorProfessorAsync(int professorId)
        {
            return await _context.Turma.CountAsync(t => t.ProfessorId == professorId);
        }

        public async Task<IEnumerable<TurmaDominio>> ListarTurmasPorProfessorAsync(int professorId)
        {
            return await _context.Turma
                .Include(t => t.Disciplina)
                .Include(t => t.Curso)
                .Include(t => t.Frequencias)
                .Where(t => t.ProfessorId == professorId)
                .ToListAsync();
        }
    }
}