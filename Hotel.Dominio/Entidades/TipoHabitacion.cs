namespace Hotel.Dominio.Entidades;

public class TipoHabitacion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int CapacidadAdultos { get; set; }
    public int CapacidadNinos { get; set; }
    public decimal PrecioBasePorNoche { get; set; }

    public int CapacidadTotal => CapacidadAdultos + CapacidadNinos;

    public ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
}
