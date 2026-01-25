using EduManager.InfraEstrutura.Data;
using EduManager.Models.Entities.Dominios;
using Microsoft.EntityFrameworkCore;

namespace EduManager.InfraEstrutura.Data
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Cursos
            modelBuilder.Entity<CursoDominio>().HasData(
                new CursoDominio { CursoId = 1, CursoNome = "Engenharia de Software", CursoDescricao = "Curso de Engenharia de Software", CoordenadorId = 1 },
                new CursoDominio { CursoId = 2, CursoNome = "Ciência da Computação", CursoDescricao = "Curso de Ciência da Computação", CoordenadorId = 1 }
            );

            // Seed Professores
            modelBuilder.Entity<ProfessorDominio>().HasData(
                new ProfessorDominio { ProfessorId = 1, ProfessorNome = "Dr. João Pereira", CPF = "12345678901", Especialidade = "Programação", Salario = 5000.00m },
                new ProfessorDominio { ProfessorId = 2, ProfessorNome = "Dra. Maria Silva", CPF = "23456789012", Especialidade = "Banco de Dados", Salario = 5500.00m }
            );

            // Seed Alunos
            modelBuilder.Entity<AlunoDominio>().HasData(
                new AlunoDominio { AlunoId = 1, AlunoNomeCompleto = "Carlos Eduardo", AlunoDataNascimento = new DateTime(2000, 5, 15), AlunoDataMatricula = new DateTime(2023, 8, 1), CursoId = 1, AlunoAtivo = true, AlunoCPF = "34567890123", AlunoEmail = "carlos@exemplo.com" },
                new AlunoDominio { AlunoId = 2, AlunoNomeCompleto = "Ana Paula", AlunoDataNascimento = new DateTime(1999, 10, 20), AlunoDataMatricula = new DateTime(2023, 8, 1), CursoId = 2, AlunoAtivo = true, AlunoCPF = "45678901234", AlunoEmail = "ana@exemplo.com" },
                new AlunoDominio { AlunoId = 3, AlunoNomeCompleto = "Lucas Mendes", AlunoDataNascimento = new DateTime(2001, 3, 10), AlunoDataMatricula = new DateTime(2023, 8, 1), CursoId = 1, AlunoAtivo = true, AlunoCPF = "56789012345", AlunoEmail = "lucas@exemplo.com" }
            );

            // Seed Coordenadores
            modelBuilder.Entity<CoordenadorDominio>().HasData(
                new CoordenadorDominio { CoordenadorId = 1, CoordenadorNome = "Prof. Roberto Lima", CoordenadorEmail = "roberto@exemplo.com", CoordenadorCPF = "67890123456", CoordenadorSalario = 6000.00m }
            );

            // Seed Disciplinas
            modelBuilder.Entity<DisciplinaDominio>().HasData(
                new DisciplinaDominio { DisciplinaId = 1, Nome = "Programação Orientada a Objetos", Codigo = "POO101", ProfessorId = 1, CursoId = 1 },
                new DisciplinaDominio { DisciplinaId = 2, Nome = "Banco de Dados", Codigo = "BD201", ProfessorId = 2, CursoId = 1 },
                new DisciplinaDominio { DisciplinaId = 3, Nome = "Algoritmos", Codigo = "ALG101", ProfessorId = 1, CursoId = 2 }
            );

            // Seed Turmas
            modelBuilder.Entity<TurmaDominio>().HasData(
                new TurmaDominio { TurmaId = 1, Nome = "Turma A - POO", Ano = "2023", DisciplinaId = 1, ProfessorId = 1, CursoId = 1 },
                new TurmaDominio { TurmaId = 2, Nome = "Turma B - BD", Ano = "2023", DisciplinaId = 2, ProfessorId = 2, CursoId = 1 },
                new TurmaDominio { TurmaId = 3, Nome = "Turma A - Alg", Ano = "2023", DisciplinaId = 3, ProfessorId = 1, CursoId = 2 }
            );

            // Seed Notas
            modelBuilder.Entity<NotaDominio>().HasData(
                new NotaDominio { NotaId = 1, Valor = 8.5m, TipoAvaliacao = "Prova 1", AlunoId = 1, DisciplinaId = 1 },
                new NotaDominio { NotaId = 2, Valor = 9.0m, TipoAvaliacao = "Prova 1", AlunoId = 2, DisciplinaId = 2 },
                new NotaDominio { NotaId = 3, Valor = 7.5m, TipoAvaliacao = "Prova 1", AlunoId = 3, DisciplinaId = 1 }
            );

            // Seed Frequências
            modelBuilder.Entity<FrequenciaDominio>().HasData(
                new FrequenciaDominio { FrequenciaId = 1, DataAula = new DateTime(2023, 9, 1), Presente = true, AlunoId = 1, DisciplinaId = 1, TurmaId = 1 },
                new FrequenciaDominio { FrequenciaId = 2, DataAula = new DateTime(2023, 9, 1), Presente = false, AlunoId = 2, DisciplinaId = 2, TurmaId = 2 },
                new FrequenciaDominio { FrequenciaId = 3, DataAula = new DateTime(2023, 9, 1), Presente = true, AlunoId = 3, DisciplinaId = 1, TurmaId = 1 }
            );
        }
    }
}