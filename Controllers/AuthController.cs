using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;
using Inventario.Models;
using Inventario.Data;
using System.Linq;
using System.Collections.Generic;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor que inyecta el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de base de datos de la aplicación</param>
    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Registro de nuevo usuario.
    /// </summary>
    /// <param name="model">Modelo con datos del nuevo usuario</param>
    /// <returns>Mensaje de éxito o error</returns>
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUser model)
    {
        // Validación de existencia previa
        if (_context.Usuarios.Any(u => u.Email == model.Email))
        {
            return BadRequest(new { message = "El correo ya está registrado." });
        }

        // Se crea un nuevo usuario con rol restringido si se intenta asignar 'Administrador'
        var usuario = new Usuario
        {
            Name = model.Name,
            Email = model.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Rol = model.Rol == "Administrador" ? "Empleado" : model.Rol
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Usuario registrado con éxito." });
    }

    /// <summary>
    /// Autenticación de usuario.
    /// </summary>
    /// <param name="model">Modelo con credenciales de login</param>
    /// <returns>Datos del usuario autenticado o error</returns>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUser model)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Name == model.Name);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuario.Password))
        {
            return Unauthorized(new { message = "Correo o contraseña incorrectos" });
        }

        usuario.IsActive = true;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Inicio de sesión exitoso",
            usuario = new
            {
                usuario.Id,
                usuario.Name,
                usuario.Email,
                usuario.Rol,
                usuario.IsActive
            }
        });
    }

    /// <summary>
    /// Obtiene todos los usuarios registrados.
    /// </summary>
    /// <returns>Lista de usuarios</returns>
    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<object>>> GetUsers()
    {
        var usuarios = await _context.Usuarios
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
                u.Rol
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    /// <summary>
    /// Actualiza el rol de un usuario. Solo permitido para administradores.
    /// </summary>
    /// <param name="id">ID del usuario a actualizar</param>
    /// <param name="model">Modelo con el nuevo rol</param>
    /// <param name="adminEmail">Correo del administrador que realiza la acción</param>
    /// <returns>Mensaje de éxito o error</returns>
    [HttpPut("update-role/{id}")]
    public async Task<ActionResult> UpdateUserRole(int id, [FromBody] UpdateRolModel model, [FromHeader] string adminEmail)
    {
        if (string.IsNullOrEmpty(model.NewRole))
        {
            return BadRequest(new { message = "El nuevo rol no puede estar vacío." });
        }

        var adminUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == adminEmail);
        if (adminUser == null || adminUser.Rol != "Administrador")
        {
            return Unauthorized(new { message = "Acceso denegado. Solo administradores pueden cambiar roles." });
        }

        var user = await _context.Usuarios.FindAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "Usuario no encontrado." });
        }

        user.Rol = model.NewRole;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Rol actualizado correctamente." });
    }
}
