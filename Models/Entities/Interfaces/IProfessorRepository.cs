using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;

namespace EduManager.Models.Entities.Interfaces
{
    public interface IProfessorRepository
    {
        Task<ProfessorDominio?> ListarProfessorPorId(int professorId);
        Task<ProfessorDominio?> ListarProfessorPorUserId(string userId);
        Task LancarNotaAsync(NotaDominio nota);
        Task RegistrarFrequenciaAsync(List<FrequenciaDominio> frequencias);
        Task<IEnumerable<AlunoDominio>> ListarAlunosPorTurmaAsync(int turmaId);
    }
}