using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Entidades;

public class Habitacion
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public int TipoHabitacionId { get; set; }
    public EstadoHabitacion EstadoOperativo { get; set; } = EstadoHabitacion.Disponible;
    public bool TieneJacuzzi { get; set; }
    public bool TieneVistaMar { get; set; }

    public TipoHabitacion? TipoHabitacion { get; set; }
    public ICollection<Reservacion> Reservaciones { get; set; } = new List<Reservacion>();

    public bool FueraDeServicio =>
        EstadoOperativo is EstadoHabitacion.EnMantenimiento or EstadoHabitacion.Bloqueada;
}
