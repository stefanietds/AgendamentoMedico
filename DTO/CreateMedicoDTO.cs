﻿namespace SistemaAgendamentoConsulta.DTO;

public class CreateMedicoDTO 
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Rg { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
    public string Crm { get; set; }
    public int EspecialidadeId { get; set; }
}