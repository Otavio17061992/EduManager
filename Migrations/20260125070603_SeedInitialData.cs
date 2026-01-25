using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduManager.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coordenadores",
                columns: new[] { "CoordenadorId", "CoordenadorCPF", "CoordenadorDataContratacao", "CoordenadorEmail", "CoordenadorNome", "CoordenadorSalario", "UserId" },
                values: new object[] { 1, "67890123456", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "roberto@exemplo.com", "Prof. Roberto Lima", 6000.00m, null });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "ProfessorId", "CPF", "DataContratacao", "Especialidade", "ProfessorNome", "Salario", "UserId" },
                values: new object[,]
                {
                    { 1, "12345678901", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programação", "Dr. João Pereira", 5000.00m, null },
                    { 2, "23456789012", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banco de Dados", "Dra. Maria Silva", 5500.00m, null }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "CursoId", "CoordenadorId", "CursoAtivo", "CursoCargaHoraria", "CursoDataInicio", "CursoDataTermino", "CursoDescricao", "CursoNome" },
                values: new object[,]
                {
                    { 1, 1, true, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curso de Engenharia de Software", "Engenharia de Software" },
                    { 2, 1, true, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curso de Ciência da Computação", "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "AlunoId", "AlunoAtivo", "AlunoCPF", "AlunoDataMatricula", "AlunoDataNascimento", "AlunoEmail", "AlunoNomeCompleto", "CursoId", "UserId" },
                values: new object[,]
                {
                    { 1, true, "34567890123", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos@exemplo.com", "Carlos Eduardo", 1, null },
                    { 2, true, "45678901234", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana@exemplo.com", "Ana Paula", 2, null },
                    { 3, true, "56789012345", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "lucas@exemplo.com", "Lucas Mendes", 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "AlunoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "AlunoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "AlunoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "ProfessorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "ProfessorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "CursoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "CursoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coordenadores",
                keyColumn: "CoordenadorId",
                keyValue: 1);
        }
    }
}
