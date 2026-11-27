using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();

    public ICollection<Reservacion> Reservaciones { get; set; } = new List<Reservacion>();
}
