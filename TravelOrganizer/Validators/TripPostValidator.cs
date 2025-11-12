using FluentValidation;
using TravelOrganizer.Dtos;

namespace TravelOrganizer.Validators;

/// <summary>
/// Validador que asegura que los datos de un viaje sean correctos antes de guardarlos.
/// </summary>
public class TripPostValidator : AbstractValidator<TripPostDto>
{
    public TripPostValidator()
    {
        // El nombre no puede estar vacío y debe tener máximo 100 caracteres
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        // El presupuesto no puede ser negativo
        RuleFor(x => x.Budget)
            .GreaterThanOrEqualTo(0);

        // La fecha de inicio no puede ser posterior a la de fin
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("La fecha de inicio debe ser anterior o igual a la fecha de fin.");
    }
}
