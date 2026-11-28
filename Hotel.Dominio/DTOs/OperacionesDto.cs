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

    public Moneda Moneda { get; set; } = Moneda.CRC;
    public decimal TipoCambioUsd { get; set; } = Reglas.ConversorMoneda.TipoCambioUsdDefecto;
    public decimal TarifaBaseUsd { get; set; }
    public decimal HospedajeUsd { get; set; }
    public decimal RecargoPersonasExtraUsd { get; set; }
    public decimal DescuentoUsd { get; set; }
    public decimal SubtotalUsd { get; set; }
    public decimal IvaUsd { get; set; }
    public decimal TotalUsd { get; set; }
    public decimal AnticipoMinimoUsd { get; set; }
}

public class CotizarReservacionDto
{
    public int HabitacionId { get; set; }
    public int? ClienteId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CantidadHuespedes { get; set; }
    public Moneda Moneda { get; set; } = Moneda.CRC;
}

public class CrearReservacionDto
{
    public int ClienteId { get; set; }
    public int HabitacionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CantidadHuespedes { get; set; }
    public string? Observaciones { get; set; }
    public Moneda Moneda { get; set; } = Moneda.CRC;
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

    public Moneda MonedaReserva { get; set; } = Moneda.CRC;
    public Moneda MonedaFacturacion { get; set; } = Moneda.CRC;
    public decimal TipoCambioUsd { get; set; } = Reglas.ConversorMoneda.TipoCambioUsdDefecto;
    public decimal HospedajeUsd => Reglas.ConversorMoneda.ColonesADolares(Hospedaje, TipoCambioUsd);
    public decimal ConsumosUsd => Reglas.ConversorMoneda.ColonesADolares(Consumos, TipoCambioUsd);
    public decimal PagadoUsd => Reglas.ConversorMoneda.ColonesADolares(Pagado, TipoCambioUsd);
    public decimal SaldoUsd => Reglas.ConversorMoneda.ColonesADolares(Saldo, TipoCambioUsd);
}

public class TipoCambioDto
{
    public string MonedaBase { get; set; } = "CRC";
    public string MonedaExtranjera { get; set; } = "USD";
    public decimal TipoCambioUsd { get; set; } = Reglas.ConversorMoneda.TipoCambioUsdDefecto;
    public DateTime FechaConsulta { get; set; } = DateTime.Now;
}

public class FacturaDto
{
    public string NumeroFactura { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; } = DateTime.Now;
    public int ReservacionId { get; set; }
    public string CodigoReservacion { get; set; } = string.Empty;
    public string ClienteNombre { get; set; } = string.Empty;
    public string ClienteDocumento { get; set; } = string.Empty;
    public string? ClienteEmail { get; set; }
    public string Habitacion { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CantidadHuespedes { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string? Observaciones { get; set; }

    public Moneda MonedaReserva { get; set; } = Moneda.CRC;
    public Moneda MonedaFacturacion { get; set; } = Moneda.CRC;
    public string SimboloMoneda { get; set; } = "₡";
    public decimal TipoCambioUsd { get; set; } = Reglas.ConversorMoneda.TipoCambioUsdDefecto;
    public bool ConversionAplicada { get; set; }

    public decimal SubtotalHospedajeFacturado { get; set; }
    public decimal IvaHospedajeFacturado { get; set; }
    public decimal HospedajeFacturado { get; set; }
    public decimal ConsumosFacturados { get; set; }
    public decimal TotalFacturado { get; set; }
    public decimal PagadoFacturado { get; set; }
    public decimal SaldoFacturado { get; set; }

    public decimal SubtotalHospedajeCrc { get; set; }
    public decimal IvaHospedajeCrc { get; set; }
    public decimal HospedajeCrc { get; set; }
    public decimal ConsumosCrc { get; set; }
    public decimal TotalGeneralCrc { get; set; }
    public decimal PagadoCrc { get; set; }
    public decimal SaldoCrc { get; set; }

    public decimal SubtotalHospedajeUsd { get; set; }
    public decimal IvaHospedajeUsd { get; set; }
    public decimal HospedajeUsd { get; set; }
    public decimal ConsumosUsd { get; set; }
    public decimal TotalGeneralUsd { get; set; }
    public decimal PagadoUsd { get; set; }
    public decimal SaldoUsd { get; set; }
}

public class RegistrarPagoDto
{
    public int ReservacionId { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public string? ReferenciaTransaccion { get; set; }
    public Moneda Moneda { get; set; } = Moneda.CRC;
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

public class LoginClienteDto
{
    public string Email { get; set; } = string.Empty;
}
