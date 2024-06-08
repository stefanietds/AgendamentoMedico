using FluentValidation;

namespace SistemaAgendamentoConsulta.Service
{
    public class BaseService
    {
        protected static Task Validate<TValidation, TDto>(TValidation validation, TDto dto)
            where TValidation : AbstractValidator<TDto>
        {
            var validationResult = validation.Validate(dto);
            if (!validationResult.IsValid)
            {
                // Concatenate error messages without unnecessary details
                var errorMessages = string.Join(",", validationResult.Errors.Select(error => error.ErrorMessage).Distinct());
                throw new ValidationException(errorMessages);
            }

            return Task.CompletedTask;
        }
    }
}
