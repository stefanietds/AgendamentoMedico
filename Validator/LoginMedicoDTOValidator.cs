using FluentValidation;
using SistemaAgendamentoConsulta.DTO;

namespace SistemaAgendamentoConsulta.Validator;

public class LoginMedicoDTOValidator : AbstractValidator<LoginMedicoDTO>
{
    public LoginMedicoDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo EMAIL não pode ser nulo nem vazio")
            .Length(6, 30)
            .WithMessage("O campo deve ter de 6 a 30 caracteres")
            .EmailAddress()
            .WithMessage("O Email não é valido");

        RuleFor(x => x.Senha)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo SENHA não pode ser nulo nem vazio")
            .Length(8, 15)
            .WithMessage("O campo deve ter no maximo 15 caracteres");
        // .Matches() ou Mush
        //.WithMessage("A senha deve ter ");
    }
    /*
    public bool ValidateSenha(string username)
    {
        return !database.Users.Any(user => user.UserName == username);
    }
    */
}