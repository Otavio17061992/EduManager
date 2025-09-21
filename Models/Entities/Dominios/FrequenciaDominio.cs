using System;
using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.Entities.Dominios
{
    public class FrequenciaDominio
    {
        [Key]
        public int FrequenciaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public DateTime Data { get; set; }
        public bool Presente { get; set; }
        public int TurmaId { get; set; }

        // Relacionamentos
        public virtual AlunoDominio? Aluno { get; set; }
        public virtual DisciplinaDominio? Disciplina { get; set; }

        // Propriedade calculada: Status da presença
        public string Status => Presente ? "Presente" : "Faltou";
    }
}