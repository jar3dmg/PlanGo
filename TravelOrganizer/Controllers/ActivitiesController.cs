using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TravelOrganizer.Dtos;
using TravelOrganizer.Services;

namespace TravelOrganizer.Controllers;

/// <summary>
/// Controlador que maneja las actividades de los viajes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController(IActivityService service, IValidator<ActivityPostDto> validator) : ControllerBase
{
    // GET /api/activities/trip/{tripId}
    [HttpGet("trip/{tripId:int}")]
    public async Task<ActionResult<List<ActivityGetDto>>> GetByTrip(int tripId) =>
        Ok(await service.GetByTripAsync(tripId));

    // GET /api/activities/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActivityGetDto>> GetById(int id)
    {
        var item = await service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // POST /api/activities
    [HttpPost]
    public async Task<ActionResult<ActivityGetDto>> Post([FromBody] ActivityPostDto dto)
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

        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/activities/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] ActivityPostDto dto)
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

    // DELETE /api/activities/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
