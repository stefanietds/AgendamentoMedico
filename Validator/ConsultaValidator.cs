using FluentValidation;
using SistemaAgendamentoConsulta.DTO.Mapping;
using SistemaAgendamentoConsulta.Model;

namespace SistemaAgendamentoConsulta.Validator;

public class ConsultaValidator : AbstractValidator<ConsultaCreateDTO>
{
    public ConsultaValidator()
    {
        RuleFor(x => x.MedicoId)
            .NotNull().NotEmpty()
            .WithMessage("Esse campo não pode ser null");
        RuleFor(x => x.PacienteId)
            .NotNull().NotEmpty()
            .WithMessage("Esse campo não pode ser null");
    }
}