namespace Hotel.Dominio.DTOs;

public class OcupacionDiaDto
{
    public DateTime Fecha { get; set; }
    public int Ocupadas { get; set; }
    public int Disponibles { get; set; }
    public int FueraDeServicio { get; set; }
}

public class ResumenOcupacionMesDto
{
    public int Anio { get; set; }
    public int Mes { get; set; }
    public int Reservaciones { get; set; }
    public int Noches { get; set; }
    public decimal Monto { get; set; }
}

public class IngresoPeriodoDto
{
    public int Anio { get; set; }
    public int Mes { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public int CantidadPagos { get; set; }
    public decimal Total { get; set; }
}

public class PlatilloSolicitadoDto
{
    public int PlatilloId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CantidadVendida { get; set; }
    public decimal Total { get; set; }
}

public class TotalPedidoDto
{
    public int PedidoId { get; set; }
    public DateTime FechaPedido { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
