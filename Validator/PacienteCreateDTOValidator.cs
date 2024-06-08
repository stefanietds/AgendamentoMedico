using FluentValidation;
using SistemaAgendamentoConsulta.DTO;

namespace SistemaAgendamentoConsulta.Validator;

public class PacienteCreateDTOValidator : AbstractValidator<PacienteCreateDTO>
{
    public PacienteCreateDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo NOME não pode ser nulo nem vazio")
            .Length(3, 50)
            .WithMessage("O tamanho minimo do campo é 3 e o maximo é 50");

        RuleFor(x => x.Cpf)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo CPF não pode ser nulo nem vazio")
            .Length(11)
            .WithMessage("O campo deve ter 11 caracteres");
        
        RuleFor(x => x.Rg)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo RG não pode ser nulo nem vazio")
            .Length(8)
            .WithMessage("O campo deve ter 11 caracteres");

        RuleFor(x => x.DataNascimento)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo RG não pode ser nulo nem vazio");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo EMAIL não pode ser nulo nem vazio")
            .Length(11)
            .WithMessage("O campo deve ter 11 caracteres")
            .EmailAddress()
            .WithMessage("O Email não é valido");

        RuleFor(x => x.Senha)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo SENHA não pode ser nulo nem vazio")
            .Length(8, 15)
            .WithMessage("O campo deve ter no maximo 15 caracteres")
             // .Matches()
            .WithMessage("A senha deve ter ");

        RuleFor(x => x.Telefone)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo Telefone não pode ser nulo nem vazio")
            .Length(11)
            .WithMessage("O campo deve ter no maximo 11 caracteres");

        RuleFor(x => x.Plano)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo SENHA não pode ser nulo nem vazio")
            .Length(8, 15)
            .WithMessage("O campo deve ter no maximo 15 caracteres")
            .IsInEnum()
            .WithMessage("O plano não é credenciado ou não existe");
    }
}