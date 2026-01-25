using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduManager.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "DisciplinaId", "CargaHoraria", "Codigo", "CursoId", "Nome", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, "POO101", 1, "Programação Orientada a Objetos", 1 },
                    { 2, 0, "BD201", 1, "Banco de Dados", 2 },
                    { 3, 0, "ALG101", 2, "Algoritmos", 1 }
                });

            migrationBuilder.InsertData(
                table: "Notas",
                columns: new[] { "NotaId", "AlunoId", "DataAvaliacao", "DisciplinaId", "TipoAvaliacao", "Valor" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Prova 1", 8.5m },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Prova 1", 9.0m },
                    { 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Prova 1", 7.5m }
                });

            migrationBuilder.InsertData(
                table: "Turmas",
                columns: new[] { "TurmaId", "Ano", "Ativa", "CursoId", "DataFim", "DataInicio", "DisciplinaId", "Nome", "ProfessorId", "Semestre" },
                values: new object[,]
                {
                    { 1, "2023", true, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Turma A - POO", 1, 0 },
                    { 2, "2023", true, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Turma B - BD", 2, 0 },
                    { 3, "2023", true, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Turma A - Alg", 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Frequencias",
                columns: new[] { "FrequenciaId", "AlunoId", "DataAula", "DisciplinaId", "Presente", "TurmaId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1 },
                    { 2, 2, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, 2 },
                    { 3, 3, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Frequencias",
                keyColumn: "FrequenciaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Frequencias",
                keyColumn: "FrequenciaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Frequencias",
                keyColumn: "FrequenciaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notas",
                keyColumn: "NotaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notas",
                keyColumn: "NotaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notas",
                keyColumn: "NotaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Turmas",
                keyColumn: "TurmaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "DisciplinaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Turmas",
                keyColumn: "TurmaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Turmas",
                keyColumn: "TurmaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "DisciplinaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Disciplinas",
                keyColumn: "DisciplinaId",
                keyValue: 2);
        }
    }
}
