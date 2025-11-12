using FluentValidation;
using TravelOrganizer.Dtos;

namespace TravelOrganizer.Validators;

/// <summary>
/// Validador que asegura que los datos del gasto sean válidos antes de guardarlos.
/// </summary>
public class ExpensePostValidator : AbstractValidator<ExpensePostDto>
{
    public ExpensePostValidator()
    {
        // Descripción obligatoria
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.")
            .MaximumLength(200);

        // Monto mayor que 0
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto debe ser mayor que 0.");

        // Fecha no futura
        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("La fecha del gasto no puede ser futura.");
    }
}
