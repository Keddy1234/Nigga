IF DB_ID('HotelDb') IS NULL
    CREATE DATABASE HotelDb;
GO

USE HotelDb;
GO

IF OBJECT_ID('DetallePedidos', 'U') IS NOT NULL DROP TABLE DetallePedidos;
IF OBJECT_ID('PedidosRestaurante', 'U') IS NOT NULL DROP TABLE PedidosRestaurante;
IF OBJECT_ID('Platillos', 'U') IS NOT NULL DROP TABLE Platillos;
IF OBJECT_ID('CategoriasPlatillo', 'U') IS NOT NULL DROP TABLE CategoriasPlatillo;
IF OBJECT_ID('Pagos', 'U') IS NOT NULL DROP TABLE Pagos;
IF OBJECT_ID('Reservaciones', 'U') IS NOT NULL DROP TABLE Reservaciones;
IF OBJECT_ID('Habitaciones', 'U') IS NOT NULL DROP TABLE Habitaciones;
IF OBJECT_ID('TiposHabitacion', 'U') IS NOT NULL DROP TABLE TiposHabitacion;
IF OBJECT_ID('Clientes', 'U') IS NOT NULL DROP TABLE Clientes;
GO

CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumento NVARCHAR(20) NOT NULL,
    NumeroDocumento NVARCHAR(20) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NULL,
    Telefono NVARCHAR(20) NULL,
    FechaRegistro DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_Clientes_NumeroDocumento UNIQUE (NumeroDocumento)
);

CREATE TABLE TiposHabitacion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(60) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    CapacidadAdultos INT NOT NULL,
    CapacidadNinos INT NOT NULL DEFAULT 0,
    PrecioBasePorNoche DECIMAL(18,2) NOT NULL,
    CONSTRAINT CK_TiposHabitacion_Precio CHECK (PrecioBasePorNoche > 0),
    CONSTRAINT CK_TiposHabitacion_Capacidad CHECK (CapacidadAdultos >= 1 AND CapacidadNinos >= 0)
);

CREATE TABLE Habitaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Numero NVARCHAR(10) NOT NULL,
    Piso INT NOT NULL,
    TipoHabitacionId INT NOT NULL,
    EstadoOperativo NVARCHAR(20) NOT NULL DEFAULT 'Disponible',
    TieneJacuzzi BIT NOT NULL DEFAULT 0,
    TieneVistaMar BIT NOT NULL DEFAULT 0,
    CONSTRAINT UQ_Habitaciones_Numero UNIQUE (Numero),
    CONSTRAINT FK_Habitaciones_TiposHabitacion FOREIGN KEY (TipoHabitacionId)
        REFERENCES TiposHabitacion(Id)
);

CREATE TABLE Reservaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(20) NOT NULL,
    ClienteId INT NOT NULL,
    HabitacionId INT NOT NULL,
    FechaInicio DATETIME2 NOT NULL,
    FechaFin DATETIME2 NOT NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'PendientePago',
    CostoTotal DECIMAL(18,2) NOT NULL,
    CantidadHuespedes INT NOT NULL,
    Observaciones NVARCHAR(500) NULL,
    FechaCheckIn DATETIME2 NULL,
    FechaCheckOut DATETIME2 NULL,
    CONSTRAINT UQ_Reservaciones_Codigo UNIQUE (Codigo),
    CONSTRAINT CK_Reservaciones_Fechas CHECK (FechaFin > FechaInicio),
    CONSTRAINT CK_Reservaciones_Huespedes CHECK (CantidadHuespedes >= 1),
    CONSTRAINT FK_Reservaciones_Clientes FOREIGN KEY (ClienteId)
        REFERENCES Clientes(Id),
    CONSTRAINT FK_Reservaciones_Habitaciones FOREIGN KEY (HabitacionId)
        REFERENCES Habitaciones(Id)
);

CREATE INDEX IX_Reservaciones_Habitacion_Fechas
    ON Reservaciones (HabitacionId, FechaInicio, FechaFin);

CREATE TABLE Pagos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    MetodoPago NVARCHAR(20) NOT NULL,
    FechaPago DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    ReferenciaTransaccion NVARCHAR(60) NULL,
    CONSTRAINT CK_Pagos_Monto CHECK (Monto > 0),
    CONSTRAINT FK_Pagos_Reservaciones FOREIGN KEY (ReservacionId)
        REFERENCES Reservaciones(Id)
);

CREATE TABLE CategoriasPlatillo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(200) NULL,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Platillos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoriaId INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(300) NULL,
    Precio DECIMAL(18,2) NOT NULL,
    TiempoPreparacionMinutos INT NOT NULL DEFAULT 15,
    Disponible BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Platillos_Precio CHECK (Precio >= 0),
    CONSTRAINT FK_Platillos_Categorias FOREIGN KEY (CategoriaId)
        REFERENCES CategoriasPlatillo(Id)
);

CREATE TABLE PedidosRestaurante (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReservacionId INT NOT NULL,
    FechaPedido DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaHoraDeseada DATETIME2 NULL,
    EstadoPedido NVARCHAR(20) NOT NULL DEFAULT 'Recibido',
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    EsRoomService BIT NOT NULL DEFAULT 1,
    Instrucciones NVARCHAR(300) NULL,
    CONSTRAINT FK_Pedidos_Reservaciones FOREIGN KEY (ReservacionId)
        REFERENCES Reservaciones(Id)
);

CREATE TABLE DetallePedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL,
    PlatilloId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT CK_DetallePedidos_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT FK_Detalle_Pedidos FOREIGN KEY (PedidoId)
        REFERENCES PedidosRestaurante(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Detalle_Platillos FOREIGN KEY (PlatilloId)
        REFERENCES Platillos(Id)
);
GO

INSERT INTO TiposHabitacion (Nombre, Descripcion, CapacidadAdultos, CapacidadNinos, PrecioBasePorNoche) VALUES
('Simple',             'Habitacion individual con Wi-Fi 6 y frigobar',              1, 0,  75000.00),
('Doble Estandar',     'Dos camas matrimoniales, Wi-Fi 6 y frigobar',               2, 1, 158000.00),
('Suite Ejecutiva',    'Cama King, sala de trabajo, jacuzzi y vista al mar',        2, 2, 310000.00),
('Suite Presidencial', 'Piso alto, jacuzzi, vista al mar, Wi-Fi 6 y frigobar',      4, 2, 700000.00),
('Familiar',           'Dos ambientes amplios, frigobar y Wi-Fi 6',                 4, 3, 376000.00);

INSERT INTO Habitaciones (Numero, Piso, TipoHabitacionId, EstadoOperativo, TieneJacuzzi, TieneVistaMar) VALUES
('101', 1, 1, 'Disponible',      0, 0),
('102', 1, 1, 'Ocupada',         0, 0),
('103', 1, 2, 'Disponible',      0, 0),
('104', 1, 2, 'EnLimpieza',      0, 0),
('105', 1, 1, 'Disponible',      0, 0),
('106', 1, 2, 'Disponible',      0, 0),
('107', 1, 5, 'Disponible',      0, 0),
('108', 1, 5, 'Disponible',      0, 0),
('201', 2, 2, 'Reservada',       0, 1),
('202', 2, 3, 'Disponible',      1, 1),
('203', 2, 5, 'Ocupada',         0, 0),
('204', 2, 5, 'EnMantenimiento', 0, 0),
('205', 2, 2, 'Disponible',      0, 1),
('206', 2, 3, 'Disponible',      1, 1),
('207', 2, 2, 'Disponible',      0, 0),
('208', 2, 1, 'Disponible',      0, 0),
('301', 3, 3, 'Disponible',      1, 1),
('302', 3, 4, 'Disponible',      1, 1),
('303', 3, 3, 'Disponible',      1, 1),
('304', 3, 5, 'Disponible',      0, 1),
('305', 3, 2, 'Disponible',      0, 1),
('306', 3, 3, 'EnLimpieza',      1, 0),
('401', 4, 4, 'Disponible',      1, 1),
('402', 4, 4, 'Disponible',      1, 1),
('403', 4, 3, 'Disponible',      1, 1),
('404', 4, 5, 'Disponible',      1, 1);

INSERT INTO Clientes (TipoDocumento, NumeroDocumento, Nombres, Apellidos, Email, Telefono) VALUES
('Cedula',    '101110111', 'Carlos',  'Mendoza Delgado', 'carlos.mendoza@correo.com', '88881111'),
('Pasaporte', '202220222', 'Ana',     'Salazar Ruiz',    'ana.salazar@correo.com',    '88882222'),
('DNI',       '402220333', 'Roberto', 'Gomez Solano',    'roberto.gomez@correo.com',  '88883333');

INSERT INTO CategoriasPlatillo (Nombre, Descripcion, Activo) VALUES
('Desayuno', 'Platillos matutinos de la posada',              1),
('Almuerzo', 'Cortes y especialidades al fogon',              1),
('Cena',     'Cenas calientes y platos de autor',             1),
('Bebidas',  'Refrescos, cafe, vinos y bebidas artesanales',  1),
('Postres',  'Reposteria tradicional del horno de lena',      1);

INSERT INTO Platillos (CategoriaId, Nombre, Descripcion, Precio, TiempoPreparacionMinutos, Disponible) VALUES
(1, 'Desayuno del Viajero',       'Huevos de granja, pan artesanal y cafe de altura', 12500.00, 15, 1),
(1, 'Omelette Rustico',           'Omelette con queso maduro, hongos y espinaca',     10800.00, 12, 1),
(1, 'Gallo Pinto Tradicional',    'Gallo pinto con huevo, natilla, platano y queso',   8500.00, 10, 1),
(1, 'Pancakes de la Abuela',      'Torre de pancakes con miel de abeja y frutos',      7900.00, 12, 1),
(2, 'Asado del Roble',            'Lomito a las brasas con papas rusticas',           26500.00, 25, 1),
(2, 'Filete de Corvina al Ajillo','Corvina fresca en mantequilla de ajo',             21000.00, 20, 1),
(2, 'Casado de la Posada',        'Arroz, frijoles, ensalada, maduro y carne',        11500.00, 15, 1),
(2, 'Pollo a la Lena',            'Medio pollo ahumado en horno de lena',             15800.00, 25, 1),
(3, 'Crema de Hongos Silvestres', 'Sopa cremosa con pan de ajo tostado',              11800.00, 15, 1),
(3, 'Fetuccini del Bosque',       'Fetuccini Alfredo con pollo a la lena',            16500.00, 18, 1),
(3, 'Costillas BBQ Ahumadas',     'Costillas de cerdo glaseadas con salsa de la casa',24500.00, 30, 1),
(3, 'Salmon a la Mantequilla',    'Salmon con mantequilla de hierbas y vegetales',    28900.00, 22, 1),
(4, 'Agua Pura de Manantial',     'Vaso o jarra de agua fresca purificada (Gratis)',     0.00,  1, 1),
(4, 'Agua con Gas y Limon',       'Agua mineralizada refrescante (Gratis)',              0.00,  2, 1),
(4, 'Fresco Natural de Cas',      'Fruta fresca natural recien batida',               3500.00,  5, 1),
(4, 'Fresco Natural de Maracuya', 'Maracuya natural ligeramente endulzado',           3500.00,  5, 1),
(4, 'Limonada con Hierbabuena',   'Natural recien preparada',                         4500.00,  5, 1),
(4, 'Te Frio Artesanal',          'Te negro con limon y miel',                        3200.00,  5, 1),
(4, 'Gaseosa',                    'Lata de 355 ml',                                   2800.00,  2, 1),
(4, 'Cafe de Altura',             'Cafe chorreado de la casa',                        2500.00,  5, 1),
(4, 'Sidra Especiada Caliente',   'Manzana artesanal con canela y clavo',             6500.00,  5, 1),
(4, 'Coctel Pina Colada Tropical','Ron blanco, crema de coco y jugo de pina',         7500.00,  6, 1),
(4, 'Coctel Mojito de la Posada', 'Ron, hierbabuena fresca, limon y soda',            7200.00,  6, 1),
(4, 'Coctel Margarita Artesanal', 'Tequila reposado, triple sec y limon',             7800.00,  6, 1),
(4, 'Coctel Sangria Frutos Rojos','Vino tinto macerado con frutas tropicales',        6900.00,  5, 1),
(4, 'Coctel Gin Tonic Botanico',  'Ginebra premium con agua tonica y pepino',         8200.00,  5, 1),
(4, 'Cerveza Artesanal',          'Botella de cerveza local',                         4800.00,  2, 1),
(4, 'Copa de Vino Tinto',         'Seleccion de la cava',                             9500.00,  2, 1),
(5, 'Tarta Rustica de Manzana',   'Horneada con miel y canela',                        8900.00, 12, 1),
(5, 'Tres Leches de la Casa',     'Porcion casera con canela',                         7500.00,  5, 1),
(5, 'Volcan de Chocolate',        'Con helado de vainilla',                            8200.00, 15, 1);

INSERT INTO Reservaciones (Codigo, ClienteId, HabitacionId, FechaInicio, FechaFin, Estado, CostoTotal, CantidadHuespedes, Observaciones, FechaCheckIn) VALUES
('RES-2026-0001', 1, 2, DATEADD(DAY, -1, CAST(GETDATE() AS DATE)), DATEADD(DAY, 2, CAST(GETDATE() AS DATE)), 'EnCurso',    261000.00, 1, 'Piso bajo', SYSDATETIME()),
('RES-2026-0002', 2, 5, DATEADD(DAY,  1, CAST(GETDATE() AS DATE)), DATEADD(DAY, 4, CAST(GETDATE() AS DATE)), 'Confirmada', 549840.00, 2, 'Vista al mar', NULL),
('RES-2026-0003', 3, 7, DATEADD(DAY, -2, CAST(GETDATE() AS DATE)), DATEADD(DAY, 1, CAST(GETDATE() AS DATE)), 'EnCurso',   1308480.00, 4, NULL, DATEADD(DAY, -2, SYSDATETIME()));

INSERT INTO Pagos (ReservacionId, Monto, MetodoPago, ReferenciaTransaccion) VALUES
(1, 150000.00, 'Efectivo', NULL),
(2, 200000.00, 'Tarjeta', 'AUT-44821'),
(3, 600000.00, 'Transferencia', 'TRF-99201');

INSERT INTO PedidosRestaurante (ReservacionId, EstadoPedido, Total, EsRoomService, Instrucciones) VALUES
(1, 'Entregado', 34100.00, 1, 'Sin cebolla');

INSERT INTO DetallePedidos (PedidoId, PlatilloId, Cantidad, PrecioUnitario, Subtotal) VALUES
(1, 3, 1, 26500.00, 26500.00),
(1, 8, 1,  4500.00,  4500.00);
GO
