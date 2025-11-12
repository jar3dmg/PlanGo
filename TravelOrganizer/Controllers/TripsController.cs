using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Dtos;
using TravelOrganizer.Services;

namespace TravelOrganizer.Controllers;

/// <summary>
/// Controlador que expone los endpoints HTTP para la entidad Trip.
/// Permite crear, consultar, actualizar y eliminar viajes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TripsController(ITripService service, IValidator<TripPostDto> validator) : ControllerBase
{
    // GET: /api/trips → Obtiene todos los viajes
    [HttpGet]
    public async Task<ActionResult<List<TripGetDto>>> Get() =>
        Ok(await service.GetAllAsync());

    // GET: /api/trips/{id} → Obtiene un viaje por ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TripGetDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // POST: /api/trips → Crea un nuevo viaje
    [HttpPost]
    public async Task<ActionResult<TripGetDto>> Post([FromBody] TripPostDto dto)
    {
        var result = await validator.ValidateAsync(dto);

        // Si la validación falla, construimos manualmente el resultado en formato JSON
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new ValidationProblemDetails(errors)
            {
                Title = "Error de validación",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: /api/trips/{id} → Actualiza un viaje existente
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] TripPostDto dto)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return BadRequest(new ValidationProblemDetails(errors)
            {
                Title = "Error de validación",
                Status = StatusCodes.Status400BadRequest
            });
        }

        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE: /api/trips/{id} → Elimina un viaje
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    // GET /api/trips/{id}/balance → Obtiene el balance del viaje
    [HttpGet("{id:int}/balance")]
    public async Task<ActionResult<TripBalanceDto>> GetTripBalance(int id)
    {
        var balance = await service.GetTripBalanceAsync(id);
        return balance is null ? NotFound() : Ok(balance);
    }

    // GET /api/trips/{id}/summary → Devuelve un resumen completo del viaje
    [HttpGet("{id:int}/summary")]
    public async Task<ActionResult<TripSummaryDto>> GetTripSummary(int id)
    {
        var summary = await service.GetTripSummaryAsync(id);
        return summary is null ? NotFound() : Ok(summary);
    }
}
