using System.Data;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.Reglas;
using Hotel.Dominio.ValueObjects;

namespace Hotel.LogicaNegocios.Servicios;

public class ReservacionServicio : IReservacionServicio
{
    private const int EstadiasParaClienteFrecuente = 3;
    private readonly IUnidadTrabajo _uow;

    public ReservacionServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<Reservacion>> BuscarAsync(FiltroReservacionDto filtro) => _uow.Reservaciones.BuscarAsync(filtro);

    public async Task<Reservacion> ObtenerAsync(int id)
        => await _uow.Reservaciones.ObtenerDetalleAsync(id) ?? throw new NoEncontradoException("Reservación no encontrada.");

    public async Task<Reservacion> ObtenerPorCodigoAsync(string codigo)
        => await _uow.Reservaciones.ObtenerPorCodigoAsync(codigo.Trim().ToUpper())
           ?? throw new NoEncontradoException("No existe una reservación con ese código.");

    public Task<List<Reservacion>> ActivasPorDocumentoAsync(string numeroDocumento)
        => _uow.Reservaciones.ListarActivasPorDocumentoAsync(numeroDocumento.Trim());

    public async Task<CotizacionDto> CotizarAsync(CotizarReservacionDto dto)
    {
        var rango = new RangoFechas(dto.FechaInicio, dto.FechaFin);
        var hab = await _uow.Habitaciones.ObtenerConTipoAsync(dto.HabitacionId)
                  ?? throw new NoEncontradoException("Habitación no encontrada.");
        ValidarCapacidad(hab, dto.CantidadHuespedes);

        bool frecuente = dto.ClienteId.HasValue && await EsClienteFrecuenteAsync(dto.ClienteId.Value);
        return CalculadoraCosto.Calcular(rango, hab.TipoHabitacion!.PrecioBasePorNoche, dto.CantidadHuespedes, frecuente);
    }

    public async Task<Reservacion> CrearAsync(CrearReservacionDto dto)
    {
        var rango = new RangoFechas(dto.FechaInicio, dto.FechaFin);
        if (rango.Entrada < DateTime.Today)
            throw new ReglaNegocioException("No se permiten reservaciones con fecha de entrada pasada.");

        if (await _uow.Clientes.ObtenerPorIdAsync(dto.ClienteId) == null)
            throw new NoEncontradoException("Cliente no encontrado.");

        // Serializable: la verificación de traslape y la inserción quedan protegidas frente a reservas simultáneas
        await _uow.IniciarTransaccionAsync(IsolationLevel.Serializable);
        try
        {
            var hab = await _uow.Habitaciones.ObtenerConTipoAsync(dto.HabitacionId)
                      ?? throw new NoEncontradoException("Habitación no encontrada.");
            ValidarHabitacionReservable(hab);
            ValidarCapacidad(hab, dto.CantidadHuespedes);

            if (await _uow.Reservaciones.ExisteTraslapeAsync(hab.Id, rango.Entrada, rango.Salida))
                throw new ConflictoException("La habitación ya tiene una reservación en esas fechas.");

            bool frecuente = await EsClienteFrecuenteAsync(dto.ClienteId);
            var cotizacion = CalculadoraCosto.Calcular(rango, hab.TipoHabitacion!.PrecioBasePorNoche, dto.CantidadHuespedes, frecuente);

            var reservacion = new Reservacion
            {
                Codigo = await GenerarCodigoAsync(rango.Entrada.Year),
                ClienteId = dto.ClienteId,
                HabitacionId = hab.Id,
                FechaInicio = dto.FechaInicio,
                FechaFin = rango.Salida,
                CantidadHuespedes = dto.CantidadHuespedes,
                Observaciones = dto.Observaciones,
                CostoTotal = cotizacion.Total,
                Estado = EstadoReservacion.PendientePago
            };

            await _uow.Reservaciones.AgregarAsync(reservacion);
            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
            return reservacion;
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task ModificarAsync(int id, CrearReservacionDto dto)
    {
        var rango = new RangoFechas(dto.FechaInicio, dto.FechaFin);

        await _uow.IniciarTransaccionAsync(IsolationLevel.Serializable);
        try
        {
            var reservacion = await _uow.Reservaciones.ObtenerPorIdAsync(id)
                              ?? throw new NoEncontradoException("Reservación no encontrada.");
            if (reservacion.Estado is not (EstadoReservacion.PendientePago or EstadoReservacion.Confirmada))
                throw new ReglaNegocioException($"No se puede modificar una reservación en estado {reservacion.Estado}.");
            if (rango.Entrada < DateTime.Today)
                throw new ReglaNegocioException("No se permiten fechas de entrada pasadas.");

            var hab = await _uow.Habitaciones.ObtenerConTipoAsync(dto.HabitacionId)
                      ?? throw new NoEncontradoException("Habitación no encontrada.");
            ValidarHabitacionReservable(hab);
            ValidarCapacidad(hab, dto.CantidadHuespedes);

            if (await _uow.Reservaciones.ExisteTraslapeAsync(hab.Id, rango.Entrada, rango.Salida, id))
                throw new ConflictoException("La habitación ya tiene una reservación en esas fechas.");

            bool frecuente = await EsClienteFrecuenteAsync(reservacion.ClienteId);
            var cotizacion = CalculadoraCosto.Calcular(rango, hab.TipoHabitacion!.PrecioBasePorNoche, dto.CantidadHuespedes, frecuente);

            reservacion.HabitacionId = hab.Id;
            reservacion.FechaInicio = dto.FechaInicio;
            reservacion.FechaFin = rango.Salida;
            reservacion.CantidadHuespedes = dto.CantidadHuespedes;
            reservacion.Observaciones = dto.Observaciones;
            reservacion.CostoTotal = cotizacion.Total;

            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task CancelarAsync(int id)
    {
        var reservacion = await _uow.Reservaciones.ObtenerPorIdAsync(id)
                          ?? throw new NoEncontradoException("Reservación no encontrada.");
        if (reservacion.Estado is not (EstadoReservacion.PendientePago or EstadoReservacion.Confirmada))
            throw new ReglaNegocioException($"No se puede cancelar una reservación en estado {reservacion.Estado}.");

        reservacion.Estado = EstadoReservacion.Cancelada;
        await _uow.GuardarAsync();
    }

    public async Task CheckInAsync(int id, CheckInDto dto)
    {
        await _uow.IniciarTransaccionAsync();
        try
        {
            var reservacion = await _uow.Reservaciones.ObtenerDetalleAsync(id)
                              ?? throw new NoEncontradoException("Reservación no encontrada.");

            if (reservacion.Estado != EstadoReservacion.Confirmada)
                throw new ReglaNegocioException("Solo se permite el check-in de reservaciones confirmadas.");
            if (reservacion.HabitacionId != dto.HabitacionId)
                throw new ReglaNegocioException("La habitación no corresponde a la reservación.");

            // Política de llegada: desde el día de entrada hasta un día después
            var hoy = DateTime.Today;
            if (hoy < reservacion.FechaInicio.Date || hoy > reservacion.FechaInicio.Date.AddDays(1))
                throw new ReglaNegocioException("El check-in está fuera de la política de llegada.");

            var hab = reservacion.Habitacion!;
            if (hab.FueraDeServicio || hab.EstadoOperativo == EstadoHabitacion.Ocupada)
                throw new ReglaNegocioException($"La habitación no está lista (estado: {hab.EstadoOperativo}).");

            reservacion.Estado = EstadoReservacion.EnCurso;
            reservacion.FechaCheckIn = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(dto.Acompanantes) || !string.IsNullOrWhiteSpace(dto.NumeroLlave))
                reservacion.Observaciones = $"{reservacion.Observaciones} | Acompañantes: {dto.Acompanantes} | Llave: {dto.NumeroLlave}".Trim(' ', '|');
            hab.EstadoOperativo = EstadoHabitacion.Ocupada;

            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task<EstadoCuentaDto> CheckOutAsync(int id)
    {
        await _uow.IniciarTransaccionAsync(IsolationLevel.RepeatableRead);
        try
        {
            var reservacion = await _uow.Reservaciones.ObtenerDetalleAsync(id)
                              ?? throw new NoEncontradoException("Reservación no encontrada.");
            if (reservacion.Estado != EstadoReservacion.EnCurso || reservacion.FechaCheckIn == null)
                throw new ReglaNegocioException("No se puede hacer check-out sin check-in previo.");

            var cuenta = await CalcularCuentaAsync(reservacion);
            if (cuenta.Saldo > 0)
                throw new ReglaNegocioException($"Hay un saldo pendiente de {cuenta.Saldo:N2}. Registre el pago antes del check-out.");

            reservacion.Estado = EstadoReservacion.Completada;
            reservacion.FechaCheckOut = DateTime.Now;
            reservacion.Habitacion!.EstadoOperativo = EstadoHabitacion.EnLimpieza;

            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
            return cuenta;
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }

    public async Task<EstadoCuentaDto> EstadoCuentaAsync(int id)
    {
        var reservacion = await ObtenerAsync(id);
        return await CalcularCuentaAsync(reservacion);
    }

    private async Task<EstadoCuentaDto> CalcularCuentaAsync(Reservacion r) => new()
    {
        ReservacionId = r.Id,
        Codigo = r.Codigo,
        Hospedaje = r.CostoTotal,
        Consumos = await _uow.Pedidos.TotalConsumosAsync(r.Id),
        Pagado = await _uow.Pagos.TotalPagadoAsync(r.Id)
    };

    private async Task<bool> EsClienteFrecuenteAsync(int clienteId)
        => await _uow.Clientes.ContarEstadiasCompletadasAsync(clienteId) >= EstadiasParaClienteFrecuente;

    private async Task<string> GenerarCodigoAsync(int anio)
    {
        int siguiente = await _uow.Reservaciones.ContarDelAnioAsync(anio) + 1;
        return $"RES-{anio}-{siguiente:D4}";
    }

    private static void ValidarHabitacionReservable(Habitacion hab)
    {
        if (hab.FueraDeServicio)
            throw new ReglaNegocioException("La habitación está fuera de servicio o en mantenimiento.");
    }

    private static void ValidarCapacidad(Habitacion hab, int huespedes)
    {
        if (huespedes < 1)
            throw new ReglaNegocioException("La cantidad de huéspedes debe ser al menos 1.");
        if (huespedes > hab.TipoHabitacion!.CapacidadTotal)
            throw new ReglaNegocioException($"La habitación admite como máximo {hab.TipoHabitacion.CapacidadTotal} huéspedes.");
    }
}
