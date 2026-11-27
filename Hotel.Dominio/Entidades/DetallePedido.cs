namespace Hotel.Dominio.Entidades;

public class DetallePedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int PlatilloId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }

    public PedidoRestaurante? Pedido { get; set; }
    public Platillo? Platillo { get; set; }
}
