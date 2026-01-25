using System;
using System.ComponentModel.DataAnnotations;

namespace EduManager.Models.ViewModels
{
    public class LancarNotaViewModel
    {
        [Required(ErrorMessage = "O aluno é obrigatório.")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "A disciplina é obrigatória.")]
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "O valor da nota é obrigatório.")]
        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo de avaliação é obrigatório.")]
        public string TipoAvaliacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data da avaliação é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;

        public int? TurmaId { get; set; }
    }
}
