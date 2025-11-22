using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;
using Inventario.Models;

namespace Inventario.Controllers;

[ApiController]
[Route("api/ventas")]
public class VentasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VentasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/ventas
    [HttpGet]
    public async Task<IActionResult> GetVentas()
    {
        var ventas = await _context.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .ToListAsync();

        var response = ventas.Select(v => new {
            id = v.Id,
            fecha = v.Fecha,
            total = v.Total,
            estado = v.Total > 0 ? "Completado" : "Pendiente",
            metodoPago = v.MetodoPago,
            cliente = v.Cliente,
            rfc = v.RFC,
            direccionFiscal = v.DireccionFiscal,
            correoFactura = v.CorreoFactura,
            usoCfdi = v.UsoCFDI,
            razonSocial = v.RazonSocial,
            detalles = v.Detalles.Select(d => new {
                producto = d.Producto.Name,
                cantidad = d.Cantidad,
                precioUnitario = d.PrecioUnitario,
                subtotal = d.Subtotal
            })
        });

        return Ok(response);
    }

    // POST: api/ventas
    [HttpPost]
    public async Task<IActionResult> CrearVenta([FromBody] VentaDTO dto)
    {
        var nuevaVenta = new Venta {
        Fecha = DateTime.Now,
        MetodoPago = dto.MetodoPago,
        Cliente = dto.Cliente,
        RFC = dto.RFC,
        DireccionFiscal = dto.DireccionFiscal,
        CorreoFactura = dto.CorreoFactura,
        UsoCFDI = dto.UsoCFDI,
        RazonSocial = dto.RazonSocial,
        Detalles = dto.Detalles.Select(d => new DetalleVenta
        {
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Cantidad * d.PrecioUnitario
        }).ToList()
};

        nuevaVenta.Total = nuevaVenta.Detalles.Sum(d => d.Subtotal);

        nuevaVenta.Estado = (nuevaVenta.Total > 0) ? "Completado" : "Pendiente";

        // Obtener el último ID para generar el folio
        var ultimoId = await _context.Ventas.MaxAsync(v => (int?)v.Id) ?? 0;
        nuevaVenta.Folio = $"FORRA-{(ultimoId + 1).ToString("D4")}";


        _context.Ventas.Add(nuevaVenta);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Venta registrada correctamente", ventaId = nuevaVenta.Id });
    }

    // Endpoint para filtrar las ventas
    [HttpGet("filtradas")]
    public async Task<IActionResult> GetVentasFiltradas(
        [FromQuery] int? year,
        [FromQuery] int? mes,
        [FromQuery] string? estado,
        [FromQuery] decimal? minTotal,
        [FromQuery] decimal? maxTotal,
        [FromQuery] string? producto)
    {
        try
        {
            var query = _context.Ventas
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .AsQueryable();

            if (year.HasValue)
                query = query.Where(v => v.Fecha.Year == year);

            if (mes.HasValue && mes.Value >= 1 && mes.Value <= 12)
                query = query.Where(v => v.Fecha.Month == mes);

            if (!string.IsNullOrEmpty(estado))
            {
                if (estado == "Completado")
                    query = query.Where(v => v.Total > 0);
                else if (estado == "Pendiente")
                    query = query.Where(v => v.Total == 0);
            }

            if (minTotal.HasValue)
                query = query.Where(v => v.Total >= minTotal);

            if (maxTotal.HasValue)
                query = query.Where(v => v.Total <= maxTotal);

            if (!string.IsNullOrEmpty(producto))
                query = query.Where(v => v.Detalles.Any(d => d.Producto.Name.Contains(producto)));

            var ventas = await query.ToListAsync();

            var response = ventas.Select(v => new
            {
                id = v.Id,
                folio = v.Folio,
                fecha = v.Fecha,
                total = v.Total,
                metodoPago = v.MetodoPago,
                estado = v.Total > 0 ? "Completado" : "Pendiente",
                cliente = v.Cliente,
                rfc = v.RFC,
                razonSocial = v.RazonSocial,
                direccionFiscal = v.DireccionFiscal,
                correoFactura = v.CorreoFactura,
                usoCfdi = v.UsoCFDI, // Aquí el nombre está bien si existe en el modelo
                detalles = v.Detalles.Select(d => new
                {
                    producto = d.Producto.Name,
                    cantidad = d.Cantidad,
                    precioUnitario = d.PrecioUnitario,
                    subtotal = d.Subtotal
                })
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message, stack = ex.StackTrace });
        }
    }


    // PUT: api/ventas/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarVenta(int id, [FromBody] VentaDTO dto)
    {
        var ventaExistente = await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (ventaExistente == null)
            return NotFound(new { mensaje = "Venta no encontrada" });

        // Actualizar campos simples
        ventaExistente.Fecha = dto.Fecha ?? ventaExistente.Fecha;
        ventaExistente.MetodoPago = dto.MetodoPago ?? ventaExistente.MetodoPago;
        ventaExistente.Cliente = dto.Cliente ?? ventaExistente.Cliente;
        ventaExistente.RFC = dto.RFC ?? ventaExistente.RFC;
        ventaExistente.DireccionFiscal = dto.DireccionFiscal ?? ventaExistente.DireccionFiscal;
        ventaExistente.CorreoFactura = dto.CorreoFactura ?? ventaExistente.CorreoFactura;
        ventaExistente.UsoCFDI = dto.UsoCFDI ?? ventaExistente.UsoCFDI;
        ventaExistente.RazonSocial = dto.RazonSocial ?? ventaExistente.RazonSocial;

        // Eliminar detalles anteriores
        _context.DetallesVenta.RemoveRange(ventaExistente.Detalles);

        // Agregar los nuevos detalles
        ventaExistente.Detalles = dto.Detalles.Select(d => new DetalleVenta
        {
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Cantidad * d.PrecioUnitario
        }).ToList();

        // Calcular el nuevo total
        ventaExistente.Total = ventaExistente.Detalles.Sum(d => d.Subtotal);

        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Venta actualizada correctamente" });
    }

    // DELETE: api/ventas/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarVenta(int id)
    {
        var venta = await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (venta == null)
            return NotFound(new { mensaje = "Venta no encontrada" });

        _context.DetallesVenta.RemoveRange(venta.Detalles);
        _context.Ventas.Remove(venta);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Venta eliminada correctamente" });
    }
}
