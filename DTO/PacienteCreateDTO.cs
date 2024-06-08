namespace SistemaAgendamentoConsulta.DTO;

public class PacienteCreateDTO
{
    public string Nome { get; set; }
    public string Cpf { get; set; } = null!;
    public string Rg { get; set; } = null!;
    public DateOnly DataNascimento { get; set; }
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Plano { get; set; } = null!;
}