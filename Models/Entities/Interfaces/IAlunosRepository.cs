using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities;
using EduManager.Models.Entities.Dominios;

namespace EduManager.Models.Entities.Interfaces
{
    public interface IAlunosRepository
    {
        Task<IEnumerable<AlunoDominio>> ListarAsync();
        Task<AlunoDominio?> ObterPorIdAsync(int id);
        Task<AlunoDominio?> ObterPorEmailAsync(string email);
        Task<AlunoDominio?> ObterPorCpfAsync(string cpf);
        Task<AlunoDominio?> ObterPorNomeAsync(string nome);
        Task AdicionarAsync(AlunoDominio aluno);
        Task AtualizarAsync(AlunoDominio aluno);
        Task RemoverAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}