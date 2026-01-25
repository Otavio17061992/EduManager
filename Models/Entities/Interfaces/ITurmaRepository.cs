using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;

namespace EduManager.Models.Entities.Interfaces
{
    public interface ITurmaRepository
    {
        Task AdicionarAsync(TurmaDominio turma);
        Task AtualizarAsync(TurmaDominio turma);
        Task RemoverAsync(int id);
        Task<TurmaDominio?> ObterPorIdAsync(int id);
        Task<IEnumerable<TurmaDominio>> ListarAsync();
        Task<bool> ExisteAsync(int id);
        Task<int> ContarTurmasPorProfessorAsync(int professorId);
        Task<IEnumerable<TurmaDominio>> ListarTurmasPorProfessorAsync(int professorId);
    }
}