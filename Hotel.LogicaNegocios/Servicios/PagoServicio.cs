using System.Data;
using Hotel.Dominio.DTOs;
using Hotel.Dominio.Entidades;
using Hotel.Dominio.Enums;
using Hotel.Dominio.Excepciones;
using Hotel.Dominio.Interfaces;
using Hotel.Dominio.Reglas;

namespace Hotel.LogicaNegocios.Servicios;

public class PagoServicio : IPagoServicio
{
    private readonly IUnidadTrabajo _uow;

    public PagoServicio(IUnidadTrabajo uow)
    {
        _uow = uow;
    }

    public Task<List<Pago>> ListarPorReservacionAsync(int reservacionId) => _uow.Pagos.ListarPorReservacionAsync(reservacionId);

    public async Task<Pago> RegistrarAsync(RegistrarPagoDto dto)
    {
        if (dto.Monto <= 0)
            throw new ReglaNegocioException("El monto debe ser mayor que cero.");
        if (dto.MetodoPago != MetodoPago.Efectivo && string.IsNullOrWhiteSpace(dto.ReferenciaTransaccion))
            throw new ReglaNegocioException("Debe indicar la referencia de la transacción.");

        await _uow.IniciarTransaccionAsync(IsolationLevel.RepeatableRead);
        try
        {
            var reservacion = await _uow.Reservaciones.ObtenerPorIdAsync(dto.ReservacionId)
                              ?? throw new NoEncontradoException("Reservación no encontrada.");
            if (reservacion.Estado is EstadoReservacion.Cancelada or EstadoReservacion.Completada)
                throw new ReglaNegocioException($"No se pueden registrar pagos en una reservación {reservacion.Estado}.");

            decimal consumos = await _uow.Pedidos.TotalConsumosAsync(reservacion.Id);
            decimal pagado = await _uow.Pagos.TotalPagadoAsync(reservacion.Id);
            decimal saldo = reservacion.CostoTotal + consumos - pagado;

            if (dto.Monto > saldo)
                throw new ReglaNegocioException($"El monto excede el saldo pendiente ({saldo:N2}).");

            var pago = new Pago
            {
                ReservacionId = reservacion.Id,
                Monto = dto.Monto,
                MetodoPago = dto.MetodoPago,
                ReferenciaTransaccion = dto.ReferenciaTransaccion,
                FechaPago = DateTime.Now
            };
            await _uow.Pagos.AgregarAsync(pago);

            // Se confirma la reservación al cubrir el anticipo mínimo
            if (reservacion.Estado == EstadoReservacion.PendientePago &&
                pagado + dto.Monto >= Math.Round(reservacion.CostoTotal * CalculadoraCosto.PorcentajeAnticipo, 2))
            {
                reservacion.Estado = EstadoReservacion.Confirmada;
            }

            await _uow.GuardarAsync();
            await _uow.ConfirmarAsync();
            return pago;
        }
        catch
        {
            await _uow.RevertirAsync();
            throw;
        }
    }
}
