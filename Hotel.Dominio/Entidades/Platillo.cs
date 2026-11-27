namespace Hotel.Dominio.Entidades;

public class Platillo
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int TiempoPreparacionMinutos { get; set; }
    public bool Disponible { get; set; } = true;

    public CategoriaPlatillo? Categoria { get; set; }
}
