using Microsoft.AspNetCore.Identity;

namespace EduManager.Models.Entities.Dominios;

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
    public virtual ProfessorDominio? Professor { get; set; }
    public virtual AlunoDominio? Aluno { get; set; }
    public virtual CoordenadorDominio? Coordenador { get; set; }

    // Propriedades calculadas
    public string? NomeCompleto => $"{Nome} {Sobrenome}";
    public int Idade => DateTime.Now.Year - DataNascimento.Year;
}