using FluentValidation;
using TravelOrganizer.Dtos;

namespace TravelOrganizer.Validators;

/// <summary>
/// Validador que asegura que los datos de la actividad sean válidos antes de guardarlos.
/// </summary>
public class ActivityPostValidator : AbstractValidator<ActivityPostDto>
{
    public ActivityPostValidator()
    {
        // Título obligatorio y con longitud máxima
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(120);

        // Validar que las fechas tengan sentido
        RuleFor(x => x.EndDateTime)
            .GreaterThan(x => x.StartDateTime)
            .WithMessage("La fecha de fin debe ser posterior a la de inicio.");

        // Costo no negativo
        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
    }
}
