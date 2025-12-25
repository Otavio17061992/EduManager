using EduManager.Models.Entities.Dominios;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduManager.InfraEstrutura.Data;

public class EduManagerContext : IdentityDbContext<ApplicationUser>
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

    public EduManagerContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarApplicationUser(modelBuilder);
        ConfigurarCoordenador(modelBuilder);
        ConfigurarProfessor(modelBuilder);
        ConfigurarCurso(modelBuilder);
        ConfigurarAluno(modelBuilder);
        ConfigurarDisciplina(modelBuilder);
        ConfigurarTurma(modelBuilder);
        ConfigurarNota(modelBuilder);
        ConfigurarFrequencia(modelBuilder);
    }

    private void ConfigurarApplicationUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.Nome).HasMaxLength(100);
            entity.Property(u => u.Sobrenome).HasMaxLength(100);
            entity.Property(u => u.CPF).HasMaxLength(11);
            entity.Property(u => u.Telefone).HasMaxLength(20);
            
            entity.HasIndex(u => u.CPF).IsUnique();
        });
    }

    private void ConfigurarCoordenador(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoordenadorDominio>(entity =>
        {
            entity.ToTable("Coordenadores");
            entity.HasKey(c => c.CoordenadorId);
            
            entity.Property(c => c.CoordenadorNome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.CoordenadorEmail).IsRequired().HasMaxLength(150);
            entity.Property(c => c.CoordenadorCPF).IsRequired().HasMaxLength(11);
            entity.Property(c => c.CoordenadorSalario).HasPrecision(18, 2);
            entity.Property(c => c.UserId).HasMaxLength(450);

            entity.HasOne(c => c.User)
                .WithOne(u => u.Coordenador)
                .HasForeignKey<CoordenadorDominio>(c => c.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasIndex(c => c.CoordenadorEmail).IsUnique();
            entity.HasIndex(c => c.CoordenadorCPF).IsUnique();
            entity.HasIndex(c => c.UserId).IsUnique();
        });
    }

    private void ConfigurarProfessor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfessorDominio>(entity =>
        {
            entity.ToTable("Professores");
            entity.HasKey(p => p.ProfessorId);

            entity.Property(p => p.ProfessorNome).IsRequired().HasMaxLength(100);
            entity.Property(p => p.CPF).IsRequired().HasMaxLength(11);
            entity.Property(p => p.Especialidade).HasMaxLength(100);
            entity.Property(p => p.Salario).HasPrecision(18, 2);
            entity.Property(p => p.UserId).HasMaxLength(450);

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

            entity.Property(c => c.CursoNome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.CursoDescricao).HasMaxLength(500);

            entity.HasOne(c => c.Coordenador)
                .WithMany(coord => coord.Cursos)
                .HasForeignKey(c => c.CoordenadorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigurarAluno(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlunoDominio>(entity =>
        {
            entity.ToTable("Alunos");
            entity.HasKey(a => a.AlunoId);
            
            entity.Property(a => a.AlunoNomeCompleto).IsRequired().HasMaxLength(100);
            entity.Property(a => a.AlunoEmail).IsRequired().HasMaxLength(150);
            entity.Property(a => a.AlunoCPF).IsRequired().HasMaxLength(11);
            entity.Property(a => a.AlunoDataNascimento).IsRequired();
            entity.Property(a => a.UserId).HasMaxLength(450);
                
            entity.HasOne(a => a.User)
                .WithOne(u => u.Aluno)
                .HasForeignKey<AlunoDominio>(a => a.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(a => a.Curso)
                .WithMany(c => c.Alunos)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasIndex(a => a.AlunoEmail).IsUnique();
            entity.HasIndex(a => a.AlunoCPF).IsUnique();
            entity.HasIndex(a => a.UserId).IsUnique();
        });
    }

    private void ConfigurarDisciplina(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DisciplinaDominio>(entity =>
        {
            entity.ToTable("Disciplinas");
            entity.HasKey(d => d.DisciplinaId);

            entity.Property(d => d.Nome).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Codigo).IsRequired().HasMaxLength(10);

            entity.HasOne(d => d.Professor)
                .WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Curso)
                .WithMany(c => c.Disciplinas)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(d => d.Codigo).IsUnique();
        });
    }

    private void ConfigurarTurma(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TurmaDominio>(entity =>
        {
            entity.ToTable("Turmas");
            entity.HasKey(t => t.TurmaId);

            entity.Property(t => t.Nome).IsRequired().HasMaxLength(50);
            entity.Property(t => t.Ano).HasMaxLength(10);

            entity.HasOne(t => t.Curso)
                .WithMany()
                .HasForeignKey(t => t.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Disciplina)
                .WithMany(d => d.Turmas)
                .HasForeignKey(t => t.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Professor)
                .WithMany(p => p.Turmas)
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

            entity.Property(n => n.Valor).HasPrecision(5, 2);
            entity.Property(n => n.TipoAvaliacao).HasMaxLength(50);

            entity.HasOne(n => n.Aluno)
                .WithMany(a => a.Notas)
                .HasForeignKey(n => n.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(n => n.Disciplina)
                .WithMany(d => d.Notas)
                .HasForeignKey(n => n.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigurarFrequencia(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FrequenciaDominio>(entity =>
        {
            entity.ToTable("Frequencias");
            entity.HasKey(f => f.FrequenciaId);

            entity.HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Disciplina)
                .WithMany(d => d.Frequencias)
                .HasForeignKey(f => f.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Turma)
                .WithMany(t => t.Frequencias)
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
