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
        if (!Enum.IsDefined(typeof(Moneda), dto.Moneda))
            throw new ReglaNegocioException("La moneda del pago no es válida (use CRC o USD).");
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
            decimal montoCrc = ConversorMoneda.ConvertirAColones(dto.Monto, dto.Moneda);

            if (dto.Moneda == Moneda.USD && montoCrc > saldo && montoCrc - saldo <= ConversorMoneda.TipoCambioUsdDefecto * 0.01m)
                montoCrc = saldo;

            if (montoCrc > saldo)
            {
                decimal saldoUsd = ConversorMoneda.ColonesADolares(saldo);
                throw new ReglaNegocioException($"El monto excede el saldo pendiente (₡{saldo:N2} / ${saldoUsd:N2} USD).");
            }

            string? refFinal = string.IsNullOrWhiteSpace(dto.ReferenciaTransaccion)
                ? null
                : dto.ReferenciaTransaccion.Trim();
            if (dto.Moneda == Moneda.USD)
            {
                string tagUsd = $"USD ${dto.Monto:N2}";
                refFinal = string.IsNullOrWhiteSpace(refFinal) ? tagUsd : $"{refFinal} ({tagUsd})";
            }
            if (refFinal != null && refFinal.Length > 60)
                refFinal = refFinal[..60];

            var pago = new Pago
            {
                ReservacionId = reservacion.Id,
                Monto = montoCrc,
                MetodoPago = dto.MetodoPago,
                ReferenciaTransaccion = refFinal,
                FechaPago = DateTime.Now
            };
            await _uow.Pagos.AgregarAsync(pago);

            // Se confirma la reservación al cubrir el anticipo mínimo
            if (reservacion.Estado == EstadoReservacion.PendientePago &&
                pagado + montoCrc >= Math.Round(reservacion.CostoTotal * CalculadoraCosto.PorcentajeAnticipo, 2))
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
