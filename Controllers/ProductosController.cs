using Microsoft.AspNetCore.Mvc;
using Inventario.Data;
using Inventario.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductosController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/productos/5
        // (Recomendado) Añadir un Get por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }


        // POST: api/productos
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromForm] Producto producto, IFormFile? imagen)
        {
            // (Tu lógica de guardar imagen está bien)
            if (imagen != null)
            {
                var rutaCarpeta = Path.Combine(_environment.WebRootPath, "imagenes/productos");
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }

                var nombreArchivo = $"{Guid.NewGuid()}_{imagen.FileName}";
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                producto.ImagenUrl = $"{Request.Scheme}://{Request.Host}/imagenes/productos/{nombreArchivo}";
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            // Devuelve el producto creado, es una buena práctica (CreatedAtAction)
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // --- INICIO DE CÓDIGO NUEVO ---

        /// <summary>
        /// Actualiza un producto específico.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, [FromForm] Producto productoActualizado, IFormFile? imagen)
        {
            if (id != productoActualizado.Id)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado.");
            }

            // Actualizar propiedades
            productoExistente.Name = productoActualizado.Name;
            productoExistente.descripcion = productoActualizado.descripcion; // Asegúrate que tu modelo y formulario lo envíen
            productoExistente.Price = productoActualizado.Price;
            productoExistente.Stock = productoActualizado.Stock;
            
            // Lógica para actualizar la imagen (similar a la de Crear)
            if (imagen != null)
            {
                // (Opcional: aquí podrías borrar la imagen antigua si existe)

                var rutaCarpeta = Path.Combine(_environment.WebRootPath, "imagenes/productos");
                var nombreArchivo = $"{Guid.NewGuid()}_{imagen.FileName}";
                var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }
                productoExistente.ImagenUrl = $"{Request.Scheme}://{Request.Host}/imagenes/productos/{nombreArchivo}";
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content es la respuesta estándar para un PUT exitoso
        }

        /// <summary>
        /// Elimina un producto específico.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // (Opcional: aquí deberías borrar el archivo de imagen del servidor si existe)

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }

        // --- FIN DE CÓDIGO NUEVO ---


        // (Tu método de actualizar imágenes antiguas)
        [HttpPut("actualizar-imagenes")]
        public async Task<IActionResult> ActualizarImagenesAntiguas()
        {
            //...
            return Ok();
        }

    }
}