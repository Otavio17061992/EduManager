using System;

namespace EduManager.Models.Entities
{
    public class Frequencia
    {
        public int FrequenciaId { get; set; }
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public DateTime Data { get; set; }
        public bool Presente { get; set; }

        // Relacionamentos
        public virtual Aluno? Aluno { get; set; }
        public virtual Disciplina? Disciplina { get; set; }

        // Propriedade calculada: Status da presença
        public string Status => Presente ? "Presente" : "Faltou";
    }
}