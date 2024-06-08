using FluentValidation;
using SistemaAgendamentoConsulta.DTO;

namespace SistemaAgendamentoConsulta.Validator
{
    public class CreateMedicoDTOValidator : AbstractValidator<CreateMedicoDTO>
    {
        public CreateMedicoDTOValidator()
        {
            RuleFor(x => x.Nome)
             .NotNull()
             .NotEmpty()
             .WithMessage("O campo NOME não pode ser nulo nem vazio")
             .Length(3, 50)
             .WithMessage("O tamanho minimo do campo NOME é 3 e o maximo é 50");

            RuleFor(x => x.Cpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo CPF não pode ser nulo nem vazio")
                .Length(11)
                .WithMessage("O campo CPF deve ter 11 caracteres");

            RuleFor(x => x.Rg)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo RG não pode ser nulo nem vazio")
                .Length(8)
                .WithMessage("O campo RG deve ter 8 caracteres");

            RuleFor(x => x.DataNascimento)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo RG não pode ser nulo nem vazio");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo EMAIL não pode ser nulo nem vazio")
                .Length(10,50)
                .WithMessage("O campo EMAIL deve ter no maximo 50 caracteres")
                .EmailAddress()
                .WithMessage("O EMAIL não é valido");

            RuleFor(x => x.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo SENHA não pode ser nulo nem vazio")
                .Length(6, 15)
                .WithMessage("O campo SENHA deve ter no maximo 15 caracteres");
         

            RuleFor(x => x.Telefone)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Telefone não pode ser nulo nem vazio")
                .Length(11)
                .WithMessage("O campo Telefone deve ter no maximo 11 caracteres");

            RuleFor(x => x.Crm)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Crm não pode ser nulo nem vazio")
                .Length(6)
                .WithMessage("O campo Crm deve ter no maximo 6 caracteres");

            RuleFor(x => x.EspecialidadeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo Id não pode ser nulo nem vazio");

        }
    }
}
