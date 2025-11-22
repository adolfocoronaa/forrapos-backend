using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;
using Inventario.Models;

namespace Inventario.Controllers;

[ApiController]
[Route("api/compras")]

public class CompraController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompraController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearCompra([FromBody] CompraDTO dto)
    {
        // 1) Validations
        if (dto is null || dto.Detalles is null || !dto.Detalles.Any())
            return BadRequest(new { error = "Se requiere al menos un detalle." });

        if (!dto.ProveedorId.HasValue || dto.ProveedorId.Value <= 0)
            return BadRequest(new { error = "ProveedorId es requerido y debe ser > 0." });

        var proveedorExiste = await _context.Proveedores
            .AnyAsync(p => p.Id == dto.ProveedorId.Value);
        if (!proveedorExiste)
            return BadRequest(new { error = $"ProveedorId {dto.ProveedorId} no existe." });

        // 2) Map + compute
        var nuevaCompra = new Compra
        {
            Fecha = dto.Fecha ?? DateTime.Now,
            ProveedorId = dto.ProveedorId.Value, 
            Detalles = dto.Detalles.Select(d => new DetalleCompra
            {
                ProductoId     = d.ProductoId,
                Cantidad       = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                IVA            = d.IVA,
                Subtotal       = d.Cantidad * d.PrecioUnitario
            }).ToList()
        };

        nuevaCompra.Total = nuevaCompra.Detalles.Sum(d => d.Subtotal + d.IVA);

        var ultimoId = await _context.Compras.MaxAsync(c => (int?)c.IdCompra) ?? 0;
        nuevaCompra.Folio = $"FORRA-{(ultimoId + 1):D4}";

        // 3) Save
        _context.Compras.Add(nuevaCompra);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Compra registrada correctamente", compraId = nuevaCompra.IdCompra });
    }

    // Endpoint para filtrar las compras
    [HttpGet("filtradas")]
    public async Task<IActionResult> GetComprasFiltradas(
        [FromQuery] int? year,
        [FromQuery] int? mes,
        [FromQuery] string? estado,
        [FromQuery] decimal? minTotal,
        [FromQuery] decimal? maxTotal,
        [FromQuery] string? producto)
    {
        try
        {
            var query = _context.Compras
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Producto)
                .AsQueryable();

            if (year.HasValue)
                query = query.Where(c => c.Fecha.Year == year);

            if (mes.HasValue && mes.Value >= 1 && mes.Value <= 12)
                query = query.Where(c => c.Fecha.Month == mes);

            if (!string.IsNullOrEmpty(estado))
            {
                if (estado == "Completado")
                    query = query.Where(c => c.Total > 0);
                else if (estado == "Pendiente")
                    query = query.Where(c => c.Total == 0);
            }

            if (minTotal.HasValue)
                query = query.Where(c => c.Total >= minTotal);

            if (maxTotal.HasValue)
                query = query.Where(c => c.Total <= maxTotal);

            if (!string.IsNullOrEmpty(producto))
                query = query.Where(c => c.Detalles.Any(d => d.Producto.Name.Contains(producto)));

            var compras = await query.ToListAsync();

            var response = compras.Select(c => new
            {
                id = c.IdCompra,
                folio = c.Folio,
                fecha = c.Fecha,
                total = c.Total,
                estado = c.Total > 0 ? "Completado" : "Pendiente",
                proveedorId = c.ProveedorId,                           // <-- ADD THIS
                proveedor = c.Proveedor != null ? c.Proveedor.Name : null, // <-- readable name
                detalles = c.Detalles.Select(d => new
                {
                    producto = d.Producto.Name,
                    cantidad = d.Cantidad,
                    precioUnitario = d.PrecioUnitario,
                    subtotal = d.Subtotal,
                    iva = d.IVA                                         // (optional)
                })
            });

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message, stack = ex.StackTrace });
        }
    }

    /// <summary>
    /// Updates a Compra and fully replaces its Detalles.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarCompra(int id, [FromBody] CompraDTO dto) {
        if (dto is null || dto.Detalles is null || !dto.Detalles.Any())
            return BadRequest(new { error = "CompraDTO inválido: se requiere al menos un detalle." });

        var compra = await _context.Compras.FirstOrDefaultAsync(c => c.IdCompra == id);
        if (compra is null) return NotFound(new { error = $"No existe compra con Id {id}" });

        // Solo si te mandan fecha/proveedor
        if (dto.Fecha.HasValue) compra.Fecha = dto.Fecha.Value;
        if (dto.ProveedorId.HasValue && dto.ProveedorId.Value != compra.ProveedorId)
        {
            var ok = await _context.Proveedores.AnyAsync(p => p.Id == dto.ProveedorId.Value);
            if (!ok) return BadRequest(new { error = $"ProveedorId {dto.ProveedorId} no existe." });
            compra.ProveedorId = dto.ProveedorId.Value;
        }

        using var tx = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1) Borrar detalles directo en DB (sin tracking)
            await _context.Set<DetalleCompra>()
                .Where(d => d.IdCompra == id)
                .ExecuteDeleteAsync(); // EF Core 7+

            // 2) Insertar nuevos detalles con PK en default (MUY IMPORTANTE)
            var nuevosDetalles = dto.Detalles.Select(d => new DetalleCompra
            {
                // Asegúrate de NO asignar el PK existente (déjalo en default/0)
                // Ej: Id / IdDetalleCompra = 0;
                IdCompra = id,
                ProductoId = d.ProductoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                IVA = d.IVA,
                Subtotal = d.Cantidad * d.PrecioUnitario
            }).ToList();

            await _context.Set<DetalleCompra>().AddRangeAsync(nuevosDetalles);

            // 3) Recalcular total
            compra.Total = nuevosDetalles.Sum(x => x.Subtotal + x.IVA);

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            return Ok(new { mensaje = "Compra actualizada correctamente", compraId = compra.IdCompra, total = compra.Total });
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync();
            return StatusCode(500, new { error = ex.Message });
        }
    }
}