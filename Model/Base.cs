using Microsoft.AspNetCore.Identity;

namespace SistemaAgendamentoConsulta.Model;

public abstract class Base
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Cpf { get; set; }
    public string Rg { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public Role Role { get; set;}
}