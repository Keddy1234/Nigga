using Hotel.Dominio.Enums;

namespace Hotel.Dominio.Entidades;

public class Pago
{
    public int Id { get; set; }
    public int ReservacionId { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public DateTime FechaPago { get; set; } = DateTime.Now;
    public string? ReferenciaTransaccion { get; set; }

    public Reservacion? Reservacion { get; set; }
}
