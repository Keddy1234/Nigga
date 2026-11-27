using Hotel.Dominio.Enums;

namespace Hotel.Dominio.DTOs;

public class CotizacionDto
{
    public int Noches { get; set; }
    public decimal TarifaBase { get; set; }
    public decimal Hospedaje { get; set; }
    public decimal RecargoPersonasExtra { get; set; }
    public decimal Descuento { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public decimal AnticipoMinimo { get; set; }
}

public class CotizarReservacionDto
{
    public int HabitacionId { get; set; }
    public int? ClienteId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CantidadHuespedes { get; set; }
}

public class CrearReservacionDto
{
    public int ClienteId { get; set; }
    public int HabitacionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CantidadHuespedes { get; set; }
    public string? Observaciones { get; set; }
}

public class FiltroReservacionDto
{
    public int? ClienteId { get; set; }
    public int? HabitacionId { get; set; }
    public DateTime? Desde { get; set; }
    public DateTime? Hasta { get; set; }
    public EstadoReservacion? Estado { get; set; }
}

public class HabitacionDisponibleDto
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public decimal PrecioBasePorNoche { get; set; }
    public bool TieneJacuzzi { get; set; }
    public bool TieneVistaMar { get; set; }
}

public class EstadoCuentaDto
{
    public int ReservacionId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public decimal Hospedaje { get; set; }
    public decimal Consumos { get; set; }
    public decimal Pagado { get; set; }
    public decimal Saldo => Hospedaje + Consumos - Pagado;
}

public class RegistrarPagoDto
{
    public int ReservacionId { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public string? ReferenciaTransaccion { get; set; }
}

public class CheckInDto
{
    public int HabitacionId { get; set; }
    public string? Acompanantes { get; set; }
    public string? NumeroLlave { get; set; }
}

public class CrearPedidoDto
{
    public int ReservacionId { get; set; }
    public bool EsRoomService { get; set; }
    public DateTime? FechaHoraDeseada { get; set; }
    public string? Instrucciones { get; set; }
    public List<DetallePedidoDto> Detalles { get; set; } = new();
}

public class DetallePedidoDto
{
    public int PlatilloId { get; set; }
    public int Cantidad { get; set; }
}

public class FiltroPedidoDto
{
    public int? ClienteId { get; set; }
    public int? HabitacionId { get; set; }
    public DateTime? Fecha { get; set; }
    public EstadoPedido? Estado { get; set; }
}
