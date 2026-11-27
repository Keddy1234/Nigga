using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Entidades;

public class PedidoRestaurante
{
    public int Id { get; set; }
    public int ReservacionId { get; set; }
    public DateTime FechaPedido { get; set; } = DateTime.Now;
    public DateTime? FechaHoraDeseada { get; set; }
    public EstadoPedido EstadoPedido { get; set; } = EstadoPedido.Recibido;
    public decimal Total { get; set; }
    public bool EsRoomService { get; set; }
    public string? Instrucciones { get; set; }

    public Reservacion? Reservacion { get; set; }
    public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
}
