using Microsoft.AspNetCore.Identity;

namespace EduManager.Models.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Telefone { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; }

    // Relacionamentos
    public virtual Professor? Professor { get; set; }
    public virtual Aluno? Aluno { get; set; }
    public virtual Coordenador? Coordenador { get; set; }

    // Propriedades calculadas
    public string? NomeCompleto => $"{Nome} {Sobrenome}";
    public int Idade => DateTime.Now.Year - DataNascimento.Year;
}