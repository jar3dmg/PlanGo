namespace TravelOrganizer.Entities;

/// <summary>
/// Representa una actividad programada dentro de un viaje.
/// </summary>
public class Activity
{
    public int Id { get; set; }                      // Identificador de la actividad
    public int TripId { get; set; }                  // Relación con el viaje
    public string Title { get; set; } = "";          // Título o nombre de la actividad
    public string? Location { get; set; }            // Lugar donde se realiza
    public DateTime StartDateTime { get; set; }      // Fecha y hora de inicio
    public DateTime EndDateTime { get; set; }        // Fecha y hora de fin
    public decimal? Cost { get; set; }               // Costo estimado o real
    public string? Notes { get; set; }               // Notas o detalles

    // Relación con el viaje principal
    public Trip? Trip { get; set; }
}
