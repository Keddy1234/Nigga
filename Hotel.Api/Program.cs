using System.Text.Json.Serialization;
using Hotel.AccesoDatos;
using Hotel.AccesoDatos.Contexto;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;
using Hotel.LogicaNegocios.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("HotelDb")));

builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<ITipoHabitacionServicio, TipoHabitacionServicio>();
builder.Services.AddScoped<IHabitacionServicio, HabitacionServicio>();
builder.Services.AddScoped<IReservacionServicio, ReservacionServicio>();
builder.Services.AddScoped<IPagoServicio, PagoServicio>();
builder.Services.AddScoped<IPlatilloServicio, PlatilloServicio>();
builder.Services.AddScoped<IPedidoServicio, PedidoServicio>();
builder.Services.AddScoped<IReporteServicio, ReporteServicio>();

var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
    db.Database.EnsureCreated();

    if (!db.TiposHabitacion.Any())
    {
        var tipos = new List<TipoHabitacion>
        {
            new() { Nombre = "Simple", Descripcion = "Habitación individual rústica con cama Queen, Wi-Fi 6 y frigobar", CapacidadAdultos = 1, CapacidadNinos = 0, PrecioBasePorNoche = 75000m },
            new() { Nombre = "Doble Estándar", Descripcion = "Dos camas matrimoniales en madera de roble, Wi-Fi 6 y frigobar", CapacidadAdultos = 2, CapacidadNinos = 1, PrecioBasePorNoche = 158000m },
            new() { Nombre = "Suite Ejecutiva", Descripcion = "Cama King con dosel, balcón, jacuzzi térmico y vista al mar", CapacidadAdultos = 2, CapacidadNinos = 2, PrecioBasePorNoche = 310000m },
            new() { Nombre = "Suite Presidencial", Descripcion = "Piso alto de lujo, sala con chimenea, jacuzzi, vista al mar y frigobar", CapacidadAdultos = 4, CapacidadNinos = 2, PrecioBasePorNoche = 700000m },
            new() { Nombre = "Familiar", Descripcion = "Dos ambientes amplios conectados, ideal para familias y grupos", CapacidadAdultos = 4, CapacidadNinos = 3, PrecioBasePorNoche = 376000m }
        };
        db.TiposHabitacion.AddRange(tipos);
        db.SaveChanges();

        var habitaciones = new List<Habitacion>
        {
            new() { Numero = "101", Piso = 1, TipoHabitacionId = tipos[0].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "102", Piso = 1, TipoHabitacionId = tipos[0].Id, EstadoOperativo = EstadoHabitacion.Ocupada },
            new() { Numero = "103", Piso = 1, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "104", Piso = 1, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.EnLimpieza },
            new() { Numero = "105", Piso = 1, TipoHabitacionId = tipos[0].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "106", Piso = 1, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "107", Piso = 1, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "108", Piso = 1, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "201", Piso = 2, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Reservada, TieneVistaMar = true },
            new() { Numero = "202", Piso = 2, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "203", Piso = 2, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.Ocupada },
            new() { Numero = "204", Piso = 2, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.EnMantenimiento },
            new() { Numero = "205", Piso = 2, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneVistaMar = true },
            new() { Numero = "206", Piso = 2, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "207", Piso = 2, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "208", Piso = 2, TipoHabitacionId = tipos[0].Id, EstadoOperativo = EstadoHabitacion.Disponible },
            new() { Numero = "301", Piso = 3, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "302", Piso = 3, TipoHabitacionId = tipos[3].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "303", Piso = 3, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "304", Piso = 3, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneVistaMar = true },
            new() { Numero = "305", Piso = 3, TipoHabitacionId = tipos[1].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneVistaMar = true },
            new() { Numero = "306", Piso = 3, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.EnLimpieza, TieneJacuzzi = true },
            new() { Numero = "401", Piso = 4, TipoHabitacionId = tipos[3].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "402", Piso = 4, TipoHabitacionId = tipos[3].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "403", Piso = 4, TipoHabitacionId = tipos[2].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true },
            new() { Numero = "404", Piso = 4, TipoHabitacionId = tipos[4].Id, EstadoOperativo = EstadoHabitacion.Disponible, TieneJacuzzi = true, TieneVistaMar = true }
        };
        db.Habitaciones.AddRange(habitaciones);

        if (!db.Clientes.Any())
        {
            db.Clientes.AddRange(
                new Cliente { TipoDocumento = TipoDocumento.Cedula, NumeroDocumento = "101110111", Nombres = "Carlos", Apellidos = "Mendoza Delgado", Telefono = "88881111", Email = "carlos@correo.com" },
                new Cliente { TipoDocumento = TipoDocumento.Cedula, NumeroDocumento = "202220222", Nombres = "Ana", Apellidos = "Salazar Ruiz", Telefono = "88882222", Email = "ana@correo.com" }
            );
        }

        var categorias = new List<CategoriaPlatillo>
        {
            new() { Nombre = "Desayuno", Descripcion = "Platillos matutinos de la posada", Activo = true },
            new() { Nombre = "Almuerzo", Descripcion = "Cortes y especialidades del mediodía", Activo = true },
            new() { Nombre = "Cena", Descripcion = "Cenas calientes y platos nocturnos de autor", Activo = true },
            new() { Nombre = "Bebidas", Descripcion = "Agua gratis, frescos naturales, cócteles, vinos y café", Activo = true },
            new() { Nombre = "Postres", Descripcion = "Repostería tradicional del horno de leña", Activo = true }
        };
        db.CategoriasPlatillo.AddRange(categorias);
        db.SaveChanges();

        db.Platillos.AddRange(
            new Platillo { CategoriaId = categorias[0].Id, Nombre = "Desayuno del Viajero", Descripcion = "Huevos de granja, pan artesanal y mantequilla", Precio = 12500m, TiempoPreparacionMinutos = 15, Disponible = true },
            new Platillo { CategoriaId = categorias[0].Id, Nombre = "Gallo Pinto Tradicional", Descripcion = "Gallo pinto con huevos, natilla, plátano y queso", Precio = 8500m, TiempoPreparacionMinutos = 10, Disponible = true },
            new Platillo { CategoriaId = categorias[1].Id, Nombre = "Asado del Roble (Lomito)", Descripcion = "Corte de lomito a las brasas con papas rústicas", Precio = 26500m, TiempoPreparacionMinutos = 25, Disponible = true },
            new Platillo { CategoriaId = categorias[1].Id, Nombre = "Filete de Corvina al Ajillo", Descripcion = "Corvina fresca en mantequilla de ajo", Precio = 21000m, TiempoPreparacionMinutos = 20, Disponible = true },
            new Platillo { CategoriaId = categorias[2].Id, Nombre = "Crema de Hongos Silvestres", Descripcion = "Sopa cremosa artesanal con pan de ajo tostado", Precio = 11800m, TiempoPreparacionMinutos = 15, Disponible = true },
            new Platillo { CategoriaId = categorias[2].Id, Nombre = "Salmón a la Mantequilla de Hierbas", Descripcion = "Filete de salmón sellado con espárragos", Precio = 28900m, TiempoPreparacionMinutos = 22, Disponible = true },
            new Platillo { CategoriaId = categorias[3].Id, Nombre = "Agua Pura de Manantial", Descripcion = "Vaso o jarra de agua fresca purificada (Gratis)", Precio = 0m, TiempoPreparacionMinutos = 1, Disponible = true },
            new Platillo { CategoriaId = categorias[3].Id, Nombre = "Fresco Natural de Cas", Descripcion = "Fruta fresca natural recién batida", Precio = 3500m, TiempoPreparacionMinutos = 5, Disponible = true },
            new Platillo { CategoriaId = categorias[3].Id, Nombre = "Cóctel Piña Colada Tropical", Descripcion = "Ron blanco, crema de coco y jugo de piña", Precio = 7500m, TiempoPreparacionMinutos = 6, Disponible = true },
            new Platillo { CategoriaId = categorias[3].Id, Nombre = "Cóctel Mojito de la Posada", Descripcion = "Ron, hierbabuena fresca, limón y soda", Precio = 7200m, TiempoPreparacionMinutos = 6, Disponible = true },
            new Platillo { CategoriaId = categorias[4].Id, Nombre = "Tarta Rústica de Manzana", Descripcion = "Horneada al momento con miel y canela", Precio = 8900m, TiempoPreparacionMinutos = 12, Disponible = true }
        );
        db.SaveChanges();

        var clienteCarlos = db.Clientes.FirstOrDefault(c => c.Email == "carlos@correo.com");
        var hab201 = db.Habitaciones.FirstOrDefault(h => h.Numero == "201");
        if (clienteCarlos != null && hab201 != null && !db.Reservaciones.Any())
        {
            db.Reservaciones.Add(new Reservacion
            {
                Codigo = $"RES-{DateTime.Today.Year}-0001",
                ClienteId = clienteCarlos.Id,
                HabitacionId = hab201.Id,
                FechaInicio = DateTime.Today.AddDays(1),
                FechaFin = DateTime.Today.AddDays(3),
                CantidadHuespedes = 2,
                Estado = EstadoReservacion.Confirmada,
                CostoTotal = 366560m,
                Observaciones = "Reservación inicial confirmada | Modalidad: Pago de Contado"
            });
            db.SaveChanges();
        }
    }
}
catch
{
}

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (NoEncontradoException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status404NotFound;
        await ctx.Response.WriteAsJsonAsync(new { mensaje = ex.Message });
    }
    catch (ConflictoException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status409Conflict;
        await ctx.Response.WriteAsJsonAsync(new { mensaje = ex.Message });
    }
    catch (ReglaNegocioException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        await ctx.Response.WriteAsJsonAsync(new { mensaje = ex.Message });
    }
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
