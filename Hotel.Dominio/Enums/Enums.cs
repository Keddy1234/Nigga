namespace Hotel.Dominio.Enums;

public enum TipoDocumento
{
    DNI,
    Pasaporte,
    Cedula
}

public enum EstadoHabitacion
{
    Disponible,
    Reservada,
    Ocupada,
    EnLimpieza,
    EnMantenimiento,
    Bloqueada
}

public enum EstadoReservacion
{
    PendientePago,
    Confirmada,
    EnCurso,
    Completada,
    Cancelada
}

public enum MetodoPago
{
    Efectivo,
    Tarjeta,
    Transferencia
}

public enum EstadoPedido
{
    Recibido,
    EnCocina,
    EnCamino,
    Entregado,
    Cancelado
}

public enum Moneda
{
    CRC,
    USD
}
