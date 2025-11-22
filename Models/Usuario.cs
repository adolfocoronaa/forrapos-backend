using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa a un usuario dentro del sistema.
    /// Incluye credenciales, correo electrónico y rol asignado.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario (clave primaria).
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña del usuario (encriptada).
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Rol asignado al usuario (por ejemplo: Empleado, Administrador).
        /// </summary>
        public string Rol { get; set; } = "Empleado";
        public bool IsActive { get; internal set; }
    }
}
