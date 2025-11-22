using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;
using Inventario.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Controllers
{
    /// <summary>
    /// API RESTful para operaciones relacionadas con usuarios.
    /// Permite obtener y registrar nuevos usuarios en la base de datos.
    /// </summary>
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Contexto de base de datos</param>
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Objeto usuario a registrar</param>
        /// <returns>Usuario creado y ubicación del recurso</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }
    }
}
