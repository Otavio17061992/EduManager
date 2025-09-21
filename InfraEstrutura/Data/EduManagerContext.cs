using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;
using Microsoft.EntityFrameworkCore;

namespace EduManager.InfraEstrutura.Data;

public class EduManagerContext : DbContext
{
    private readonly DbContextOptions<EduManagerContext> _options;
    private DbSet<AlunoDominio> Aluno { get; set; }
    private DbSet<ProfessorDominio> Professor { get; set; }
    private DbSet<CursoDominio> Curso { get; set; }
    private DbSet<DisciplinaDominio> Disciplina { get; set; }
    private DbSet<TurmaDominio> Turma { get; set; }
    private DbSet<NotaDominio> Nota { get; set; }
    private DbSet<FrequenciaDominio> Frequencia { get; set; }
    private DbSet<CoordenadorDominio> Coordenador { get; set; }

    public EduManagerContext(DbContextOptions<EduManagerContext> options) : base(options)
    {
        _options = options;
    }


}
