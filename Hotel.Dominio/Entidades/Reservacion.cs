using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Entidades;

public class Reservacion
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public int ClienteId { get; set; }
    public int HabitacionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public EstadoReservacion Estado { get; set; } = EstadoReservacion.PendientePago;
    public decimal CostoTotal { get; set; }
    public int CantidadHuespedes { get; set; }
    public string? Observaciones { get; set; }
    public DateTime? FechaCheckIn { get; set; }
    public DateTime? FechaCheckOut { get; set; }

    public Cliente? Cliente { get; set; }
    public Habitacion? Habitacion { get; set; }
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    public ICollection<PedidoRestaurante> Pedidos { get; set; } = new List<PedidoRestaurante>();

    public bool EstaActiva =>
        Estado is EstadoReservacion.PendientePago or EstadoReservacion.Confirmada or EstadoReservacion.EnCurso;
}
