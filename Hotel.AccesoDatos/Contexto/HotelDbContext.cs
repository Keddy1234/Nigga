using Hotel.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Hotel.AccesoDatos.Contexto;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<TipoHabitacion> TiposHabitacion => Set<TipoHabitacion>();
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
    public DbSet<Reservacion> Reservaciones => Set<Reservacion>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<CategoriaPlatillo> CategoriasPlatillo => Set<CategoriaPlatillo>();
    public DbSet<Platillo> Platillos => Set<Platillo>();
    public DbSet<PedidoRestaurante> PedidosRestaurante => Set<PedidoRestaurante>();
    public DbSet<DetallePedido> DetallePedidos => Set<DetallePedido>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Cliente>(e =>
        {
            e.ToTable("Clientes");
            e.Property(x => x.TipoDocumento).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.NumeroDocumento).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombres).HasMaxLength(100).IsRequired();
            e.Property(x => x.Apellidos).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(150);
            e.Property(x => x.Telefono).HasMaxLength(20);
            e.HasIndex(x => x.NumeroDocumento).IsUnique();
        });

        mb.Entity<TipoHabitacion>(e =>
        {
            e.ToTable("TiposHabitacion", t => t.HasCheckConstraint("CK_TiposHabitacion_Precio", "PrecioBasePorNoche > 0"));
            e.Property(x => x.Nombre).HasMaxLength(60).IsRequired();
            e.Property(x => x.Descripcion).HasMaxLength(300);
            e.Property(x => x.PrecioBasePorNoche).HasColumnType("decimal(18,2)");
            e.Ignore(x => x.CapacidadTotal);
        });

        mb.Entity<Habitacion>(e =>
        {
            e.ToTable("Habitaciones");
            e.Property(x => x.Numero).HasMaxLength(10).IsRequired();
            e.Property(x => x.EstadoOperativo).HasConversion<string>().HasMaxLength(20);
            e.HasIndex(x => x.Numero).IsUnique();
            e.HasOne(x => x.TipoHabitacion).WithMany(t => t.Habitaciones)
                .HasForeignKey(x => x.TipoHabitacionId).OnDelete(DeleteBehavior.Restrict);
            e.Ignore(x => x.FueraDeServicio);
        });

        mb.Entity<Reservacion>(e =>
        {
            e.ToTable("Reservaciones", t => t.HasCheckConstraint("CK_Reservaciones_Fechas", "FechaFin > FechaInicio"));
            e.Property(x => x.Codigo).HasMaxLength(20).IsRequired();
            e.Property(x => x.Estado).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.CostoTotal).HasColumnType("decimal(18,2)");
            e.Property(x => x.Observaciones).HasMaxLength(500);
            e.HasIndex(x => x.Codigo).IsUnique();
            e.HasIndex(x => new { x.HabitacionId, x.FechaInicio, x.FechaFin });
            e.HasOne(x => x.Cliente).WithMany(c => c.Reservaciones)
                .HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Habitacion).WithMany(h => h.Reservaciones)
                .HasForeignKey(x => x.HabitacionId).OnDelete(DeleteBehavior.Restrict);
            e.Ignore(x => x.EstaActiva);
        });

        mb.Entity<Pago>(e =>
        {
            e.ToTable("Pagos", t => t.HasCheckConstraint("CK_Pagos_Monto", "Monto > 0"));
            e.Property(x => x.Monto).HasColumnType("decimal(18,2)");
            e.Property(x => x.MetodoPago).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.ReferenciaTransaccion).HasMaxLength(60);
            e.HasOne(x => x.Reservacion).WithMany(r => r.Pagos)
                .HasForeignKey(x => x.ReservacionId).OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<CategoriaPlatillo>(e =>
        {
            e.ToTable("CategoriasPlatillo");
            e.Property(x => x.Nombre).HasMaxLength(50).IsRequired();
            e.Property(x => x.Descripcion).HasMaxLength(200);
        });

        mb.Entity<Platillo>(e =>
        {
            e.ToTable("Platillos", t => t.HasCheckConstraint("CK_Platillos_Precio", "Precio >= 0"));
            e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            e.Property(x => x.Descripcion).HasMaxLength(300);
            e.Property(x => x.Precio).HasColumnType("decimal(18,2)");
            e.HasOne(x => x.Categoria).WithMany(c => c.Platillos)
                .HasForeignKey(x => x.CategoriaId).OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<PedidoRestaurante>(e =>
        {
            e.ToTable("PedidosRestaurante");
            e.Property(x => x.EstadoPedido).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.Total).HasColumnType("decimal(18,2)");
            e.Property(x => x.Instrucciones).HasMaxLength(300);
            e.HasOne(x => x.Reservacion).WithMany(r => r.Pedidos)
                .HasForeignKey(x => x.ReservacionId).OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<DetallePedido>(e =>
        {
            e.ToTable("DetallePedidos", t => t.HasCheckConstraint("CK_DetallePedidos_Cantidad", "Cantidad > 0"));
            e.Property(x => x.PrecioUnitario).HasColumnType("decimal(18,2)");
            e.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
            e.HasOne(x => x.Pedido).WithMany(p => p.Detalles)
                .HasForeignKey(x => x.PedidoId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Platillo).WithMany()
                .HasForeignKey(x => x.PlatilloId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
