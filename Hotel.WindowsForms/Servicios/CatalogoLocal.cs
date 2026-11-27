using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;

namespace Hotel.WindowsForms.Servicios
{
    // Datos compartidos entre pantallas cuando se trabaja sin la API encendida.
    public static class CatalogoLocal
    {
        public const int CategoriaDesayuno = 1;
        public const int CategoriaAlmuerzo = 2;
        public const int CategoriaCena = 3;
        public const int CategoriaBebidas = 4;
        public const int CategoriaPostres = 5;

        public static List<TipoHabitacion> Tipos { get; } = new()
        {
            new() { Id = 1, Nombre = "Simple", Descripcion = "Habitación individual rústica con cama Queen, Wi-Fi 6 y frigobar", CapacidadAdultos = 1, CapacidadNinos = 0, PrecioBasePorNoche = 75000m },
            new() { Id = 2, Nombre = "Doble Estándar", Descripcion = "Dos camas matrimoniales en madera de roble, Wi-Fi 6 y frigobar", CapacidadAdultos = 2, CapacidadNinos = 1, PrecioBasePorNoche = 158000m },
            new() { Id = 3, Nombre = "Suite Ejecutiva", Descripcion = "Cama King con dosel, balcón, jacuzzi térmico y vista al mar", CapacidadAdultos = 2, CapacidadNinos = 2, PrecioBasePorNoche = 310000m },
            new() { Id = 4, Nombre = "Suite Presidencial", Descripcion = "Piso alto de lujo, sala con chimenea, jacuzzi, vista al mar y frigobar", CapacidadAdultos = 4, CapacidadNinos = 2, PrecioBasePorNoche = 700000m },
            new() { Id = 5, Nombre = "Familiar", Descripcion = "Dos ambientes amplios conectados, ideal para familias y grupos", CapacidadAdultos = 4, CapacidadNinos = 3, PrecioBasePorNoche = 376000m }
        };

        public static List<Habitacion> Habitaciones { get; } = CrearHabitaciones();

        public static List<CategoriaPlatillo> Categorias { get; } = new()
        {
            new() { Id = 1, Nombre = "Desayuno", Descripcion = "Platillos matutinos de la posada", Activo = true },
            new() { Id = 2, Nombre = "Almuerzo", Descripcion = "Cortes y especialidades del mediodía", Activo = true },
            new() { Id = 3, Nombre = "Cena", Descripcion = "Cenas calientes y platos nocturnos de autor", Activo = true },
            new() { Id = 4, Nombre = "Bebidas", Descripcion = "Agua gratis, frescos naturales, cócteles, vinos y café", Activo = true },
            new() { Id = 5, Nombre = "Postres", Descripcion = "Repostería tradicional del horno de leña", Activo = true }
        };

        public static List<Platillo> Platillos { get; } = new()
        {
            // 1. DESAYUNOS (Solo comidas de desayuno)
            P(1, 1, "Desayuno del Viajero", "Huevos de granja al gusto, pan artesanal y mantequilla", 12500m, 15),
            P(2, 1, "Omelette Rústico de la Posada", "Omelette con queso maduro, hongos silvestres y espinaca", 10800m, 12),
            P(3, 1, "Gallo Pinto Tradicional", "Gallo pinto con huevos, natilla, plátano maduro y queso Turrialba", 8500m, 10),
            P(4, 1, "Pancakes de la Abuela", "Torre de pancakes esponjosos con miel de abeja y frutos rojos", 7900m, 12),
            P(5, 1, "Tostadas Francesas del Bosque", "Pan brioche dorado con canela, manzana caramelizada y almíbar", 9200m, 14),

            // 2. ALMUERZOS (Solo comidas de almuerzo)
            P(6, 2, "Asado del Roble (Lomito)", "Corte de lomito a las brasas con papas rústicas al romero", 26500m, 25),
            P(7, 2, "Filete de Corvina al Ajillo", "Corvina fresca en mantequilla de ajo con arroz y vegetales", 21000m, 20),
            P(8, 2, "Casado Tradicional de la Posada", "Arroz, frijoles, ensalada, plátano maduro, picadillo y carne a elegir", 11500m, 15),
            P(9, 2, "Pollo Ahumado a la Leña", "Medio pollo horneado a la leña con puré rústico y ensalada", 15800m, 25),
            P(10, 2, "Arroz con Mariscos de la Costa", "Arroz perfumado con camarones, calamares y almejas", 19500m, 22),

            // 3. CENAS (Solo comidas de cena)
            P(11, 3, "Crema de Hongos Silvestres", "Sopa cremosa artesanal acompañada de pan de ajo tostado", 11800m, 15),
            P(12, 3, "Fetuccini Alfredo del Bosque", "Pasta fresca en salsa cremosa con pechuga a la parrilla y parmesano", 16500m, 18),
            P(13, 3, "Costillas BBQ Ahumadas", "Costillas de cerdo glaseadas a fuego lento con elote asado", 24500m, 30),
            P(14, 3, "Salmón a la Mantequilla de Hierbas", "Filete de salmón sellado con espárragos y papas salteadas", 28900m, 22),
            P(15, 3, "Tabla Rústica de Quesos y Fiambres", "Selección de quesos maduros, jamón serrano, aceitunas y pan", 18500m, 10),

            // 4. BEBIDAS (Separadas: Agua Gratis, Frescos, Cócteles, Vinos, Cervezas)
            P(16, 4, "Agua Pura de Manantial", "Vaso o jarra de agua fresca purificada (Cortesía de la casa)", 0m, 1),
            P(17, 4, "Agua con Gas y Rodaja de Limón", "Agua mineralizada refrescante (Cortesía de la casa)", 0m, 2),
            P(18, 4, "Fresco Natural de Cas", "Fruta fresca natural recién batida", 3500m, 5),
            P(19, 4, "Fresco Natural de Maracuyá", "Maracuyá natural ligeramente endulzado", 3500m, 5),
            P(20, 4, "Limonada con Hierbabuena", "Limón mandarina natural con hojas de hierbabuena", 4500m, 5),
            P(21, 4, "Té Frío Artesanal de la Casa", "Infusión fría de té negro con limón y miel", 3200m, 4),
            P(22, 4, "Gaseosa", "Bebida gaseosa fría de 355 ml", 2800m, 2),
            P(23, 4, "Café de Altura Chorreado", "Café 100% arábica recién chorreado", 2500m, 5),
            P(24, 4, "Sidra Especiada Caliente", "Manzana artesanal con canela y clavo de olor", 6500m, 5),
            P(25, 4, "Cóctel Piña Colada Tropical", "Ron blanco, crema de coco y jugo de piña natural", 7500m, 6),
            P(26, 4, "Cóctel Mojito de la Posada", "Ron, hierbabuena fresca, limón y toque de soda", 7200m, 6),
            P(27, 4, "Cóctel Margarita Artesanal", "Tequila reposado, triple sec y limón fresco con borde de sal", 7800m, 6),
            P(28, 4, "Cóctel Sangría de Frutos Rojos", "Vino tinto de la casa macerado con frutas tropicales", 6900m, 5),
            P(29, 4, "Cóctel Gin Tonic Botánico", "Ginebra premium con agua tónica, enebro y pepino", 8200m, 5),
            P(30, 4, "Cerveza Artesanal del Roble", "Botella de cerveza artesanal tipo Amber Ale", 4800m, 2),
            P(31, 4, "Copa de Vino Tinto Reserva", "Cabernet Sauvignon de la cava de la posada", 9500m, 2),

            // 5. POSTRES
            P(32, 5, "Tarta Rústica de Manzana", "Horneada al momento con miel y canela", 8900m, 12),
            P(33, 5, "Tres Leches de la Casa", "Receta tradicional bañada en tres leches con canela", 7500m, 5),
            P(34, 5, "Volcán de Chocolate Caliente", "Bizcocho tibio de chocolate con bola de helado de vainilla", 8200m, 15)
        };

        private static Platillo P(int id, int cat, string nombre, string desc, decimal precio, int minutos) => new()
        {
            Id = id,
            CategoriaId = cat,
            Nombre = nombre,
            Descripcion = desc,
            Precio = precio,
            TiempoPreparacionMinutos = minutos,
            Disponible = true
        };

        private static List<Habitacion> CrearHabitaciones()
        {
            var datos = new (string, int, int, EstadoHabitacion, bool, bool)[]
            {
                ("101", 1, 1, EstadoHabitacion.Disponible, false, false),
                ("102", 1, 1, EstadoHabitacion.Ocupada, false, false),
                ("103", 1, 2, EstadoHabitacion.Disponible, false, false),
                ("104", 1, 2, EstadoHabitacion.EnLimpieza, false, false),
                ("105", 1, 1, EstadoHabitacion.Disponible, false, false),
                ("106", 1, 2, EstadoHabitacion.Disponible, false, false),
                ("107", 1, 5, EstadoHabitacion.Disponible, false, false),
                ("108", 1, 5, EstadoHabitacion.Disponible, false, false),
                ("201", 2, 2, EstadoHabitacion.Reservada, false, true),
                ("202", 2, 3, EstadoHabitacion.Disponible, true, true),
                ("203", 2, 5, EstadoHabitacion.Ocupada, false, false),
                ("204", 2, 5, EstadoHabitacion.EnMantenimiento, false, false),
                ("205", 2, 2, EstadoHabitacion.Disponible, false, true),
                ("206", 2, 3, EstadoHabitacion.Disponible, true, true),
                ("207", 2, 2, EstadoHabitacion.Disponible, false, false),
                ("208", 2, 1, EstadoHabitacion.Disponible, false, false),
                ("301", 3, 3, EstadoHabitacion.Disponible, true, true),
                ("302", 3, 4, EstadoHabitacion.Disponible, true, true),
                ("303", 3, 3, EstadoHabitacion.Disponible, true, true),
                ("304", 3, 5, EstadoHabitacion.Disponible, false, true),
                ("305", 3, 2, EstadoHabitacion.Disponible, false, true),
                ("306", 3, 3, EstadoHabitacion.EnLimpieza, true, false),
                ("401", 4, 4, EstadoHabitacion.Disponible, true, true),
                ("402", 4, 4, EstadoHabitacion.Disponible, true, true),
                ("403", 4, 3, EstadoHabitacion.Disponible, true, true),
                ("404", 4, 5, EstadoHabitacion.Disponible, true, true)
            };

            var lista = new List<Habitacion>();
            int id = 1;
            foreach (var (numero, piso, tipo, estado, jacuzzi, vista) in datos)
            {
                lista.Add(new Habitacion
                {
                    Id = id++,
                    Numero = numero,
                    Piso = piso,
                    TipoHabitacionId = tipo,
                    EstadoOperativo = estado,
                    TieneJacuzzi = jacuzzi,
                    TieneVistaMar = vista,
                    TipoHabitacion = Tipos.First(t => t.Id == tipo)
                });
            }
            return lista;
        }

        public static string NombreTipo(int tipoId) =>
            Tipos.FirstOrDefault(t => t.Id == tipoId)?.Nombre ?? "General";

        public static int CapacidadTipo(int tipoId)
        {
            var t = Tipos.FirstOrDefault(x => x.Id == tipoId);
            return t == null ? 2 : t.CapacidadAdultos + t.CapacidadNinos;
        }
    }
}
