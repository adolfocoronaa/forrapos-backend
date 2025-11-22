using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;
using Inventario.Models;

namespace Inventario.Controllers;

[ApiController]
[Route("api/inventario")]
public class InventarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public InventarioController(ApplicationDbContext context)
    {
        _context = context;
    }

     [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
    {
        return await _context.MovimientosInventario
                     .Include(m => m.Producto)
                     .OrderByDescending(m => m.Fecha)
                     .ToListAsync();
    }

     [HttpPost]
    public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoInventario mov)
    {
        if (mov == null) return BadRequest("Movimiento inválido.");
        if (mov.Cantidad <= 0) return BadRequest("La cantidad debe ser mayor a cero.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var producto = await _context.Productos
                .Where(p => p.Id == mov.ProductoId)
                .FirstOrDefaultAsync();

            if (producto == null) return NotFound("Producto no encontrado.");

            if (mov.Tipo == "ENTRADA")
            {
                producto.Stock += mov.Cantidad;
            }
            else
            {
                if (producto.Stock < mov.Cantidad)
                    return BadRequest("Stock insuficiente.");
                producto.Stock -= mov.Cantidad;
            }

            _context.MovimientosInventario.Add(mov);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok(new { mensaje = "Movimiento registrado", movimiento = mov });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"Error al registrar movimiento: {ex.Message}");
        }
    }
}