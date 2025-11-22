using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Inventario.Data;
using Inventario.Models;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProveedorController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            try
            {
                var proveedores = await _context.Proveedores.ToListAsync();
                return Ok(proveedores);
            }
            catch(Exception ex)
            {
                // Devuelve mensaje del error para depuración
                return StatusCode(500, new { mensaje = ex.Message, stack = ex.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetById(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound();
            return Ok(proveedor);
        }
        // Removed misplaced code block that caused compile error

        [HttpPost]
        public async Task<ActionResult<Proveedor>> Create([FromBody] Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = proveedor.Id }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Proveedor proveedor)
        {
            if (id != proveedor.Id)
                return BadRequest();

            var existing = await _context.Proveedores.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = proveedor.Name;
            existing.Telefono = proveedor.Telefono;
            existing.Email = proveedor.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Proveedores.FindAsync(id);
            if (existing == null)
                return NotFound();

            _context.Proveedores.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}