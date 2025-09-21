using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduManager.Models.Entities.Dominios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EduManager.InfraEstrutura.Data;

public class EduManagerContext : DbContext
{
    public DbSet<AlunoDominio> Aluno { get; set; }
    public DbSet<ProfessorDominio> Professor { get; set; }
    public DbSet<CursoDominio> Curso { get; set; }
    public DbSet<DisciplinaDominio> Disciplina { get; set; }
    public DbSet<TurmaDominio> Turma { get; set; }
    public DbSet<NotaDominio> Nota { get; set; }
    public DbSet<FrequenciaDominio> Frequencia { get; set; }
    public DbSet<CoordenadorDominio> Coordenador { get; set; }

    public EduManagerContext(DbContextOptions<EduManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarApplicationUser(modelBuilder);
        ConfigurarAluno(modelBuilder);
        ConfigurarProfessor(modelBuilder);
        ConfigurarCurso(modelBuilder);
        ConfigurarDisciplina(modelBuilder);
        ConfigurarTurma(modelBuilder);
        ConfigurarNota(modelBuilder);
        ConfigurarFrequencia(modelBuilder);
        ConfigurarCoordenador(modelBuilder);
    }

    private void ConfigurarApplicationUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.Nome).HasMaxLength(100);
            entity.Property(u => u.Sobrenome).HasMaxLength(100);
            entity.Property(u => u.CPF).HasMaxLength(11);
            entity.Property(u => u.Telefone).HasMaxLength(20);
            
            // Índice único para CPF
            entity.HasIndex(u => u.CPF).IsUnique();
        });
    }
    private void ConfigurarAluno(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlunoDominio>(entity =>
        {
            entity.ToTable("Alunos");
            entity.HasKey(a => a.AlunoId);

            entity.Property(a => a.NomeCompleto)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(a => a.CPF)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(a => a.DataNascimento)
                .IsRequired();

            entity.HasOne(a => a.User)
                .WithOne(u => u.Aluno)
                .HasForeignKey<AlunoDominio>(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Índices únicos
            entity.HasIndex(a => a.Email).IsUnique();
            entity.HasIndex(a => a.CPF).IsUnique();

        });
    }

    private void ConfigurarProfessor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfessorDominio>(entity =>
        {
            entity.ToTable("Professores");
            entity.HasKey(p => p.ProfessorId);

            entity.Property(p => p.ProfessorNome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.CPF)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(p => p.Especialidade)
                .HasMaxLength(100);

                        entity.HasOne(p => p.User)
                .WithOne(u => u.Professor)
                .HasForeignKey<ProfessorDominio>(p => p.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(p => p.CPF).IsUnique();
            entity.HasIndex(p => p.UserId).IsUnique();
        });

    }

    private void ConfigurarCurso(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CursoDominio>(entity =>
        {
            entity.ToTable("Cursos");
            entity.HasKey(c => c.CursoId);
            
            entity.Property(c => c.NomeCurso)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(c => c.Descricao)
                .HasMaxLength(500);
                
            entity.Property(c => c.CargaHoraria)
                .IsRequired();
                
            // Relacionamento com Coordenador
            entity.HasOne<CoordenadorDominio>()
                .WithMany()
                .HasForeignKey(c => c.CoordenadorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

    }

    private void ConfigurarDisciplina(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<DisciplinaDominio>(entity =>
        {
            entity.ToTable("Disciplinas");
            entity.HasKey(d => d.DisciplinaId);
            
            entity.Property(d => d.Nome)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(d => d.Codigo)
                .IsRequired()
                .HasMaxLength(10);
                
            entity.Property(d => d.CargaHoraria)
                .IsRequired();
                
            // Relacionamento com Curso
            entity.HasOne<CursoDominio>()
                .WithMany()
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Professor
            entity.HasOne<ProfessorDominio>()
                .WithMany()
                .HasForeignKey(d => d.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índice único para código
            entity.HasIndex(d => d.Codigo).IsUnique();
        });
    }

    private void ConfigurarTurma(ModelBuilder modelBuilder)
    {
                modelBuilder.Entity<TurmaDominio>(entity =>
        {
            entity.ToTable("Turmas");
            entity.HasKey(t => t.TurmaId);
            
            entity.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(t => t.Ano)
                .IsRequired();
                
            entity.Property(t => t.Semestre)
                .IsRequired();
                
            entity.Property(t => t.DataInicio)
                .IsRequired();
                
            entity.Property(t => t.DataFim)
                .IsRequired();

            // Relacionamento com Disciplina
            entity.HasOne<DisciplinaDominio>()
                .WithMany()
                .HasForeignKey(t => t.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Professor
            entity.HasOne<ProfessorDominio>()
                .WithMany()
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigurarNota(ModelBuilder modelBuilder)
    {
                modelBuilder.Entity<NotaDominio>(entity =>
        {
            entity.ToTable("Notas");
            entity.HasKey(n => n.NotaId);
            
            entity.Property(n => n.Valor)
                .IsRequired()
                .HasColumnType("decimal(4,2)"); 
                
            entity.Property(n => n.DataAvaliacao)
                .IsRequired();
                
            entity.Property(n => n.TipoAvaliacao)
                .IsRequired()
                .HasMaxLength(50);

            // Relacionamento com Aluno
            entity.HasOne<AlunoDominio>()
                .WithMany()
                .HasForeignKey(n => n.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Turma
            entity.HasOne<TurmaDominio>()
                .WithMany()
                .HasForeignKey(n => n.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice composto
            entity.HasIndex(n => new { n.AlunoId, n.TurmaId, n.TipoAvaliacao });
        });
    }

    private void ConfigurarFrequencia(ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<FrequenciaDominio>(entity =>
        {
            entity.ToTable("Frequencias");
            entity.HasKey(f => f.FrequenciaId);
            
            entity.Property(f => f.Data)
                .IsRequired();
                
            entity.Property(f => f.Presente)
                .IsRequired();
                

            // Relacionamento com Aluno
            entity.HasOne<AlunoDominio>()
                .WithMany()
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Turma
            entity.HasOne<TurmaDominio>()
                .WithMany()
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Índice composto para evitar duplicatas
            entity.HasIndex(f => new { f.AlunoId, f.TurmaId, f.Data }).IsUnique();
        });
    }

    private void ConfigurarCoordenador(ModelBuilder modelBuilder)
    {
                modelBuilder.Entity<CoordenadorDominio>(entity =>
        {
            entity.ToTable("Coordenadores");
            entity.HasKey(c => c.CoordenadorId);
            
            entity.Property(c => c.CoordenadorNome)
                .IsRequired()
                .HasMaxLength(100);
                
            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);
                
            entity.Property(c => c.CPF)
                .IsRequired()
                .HasMaxLength(11);
                
            entity.HasOne(c => c.User)
                .WithOne(u => u.Coordenador)
                .HasForeignKey<CoordenadorDominio>(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Índices únicos
            entity.HasIndex(c => c.Email).IsUnique();
            entity.HasIndex(c => c.CPF).IsUnique();
            entity.HasIndex(c => c.UserId).IsUnique();
        });
    }
}
