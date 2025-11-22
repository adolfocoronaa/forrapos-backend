using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    /// <summary>
    /// Modelo de datos para el registro de un nuevo usuario.
    /// Contiene información básica como nombre, email, contraseña y rol.
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        /// Nombre de usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contraseña para el acceso del usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rol asignado al usuario. Por defecto es 'Empleado'.
        /// </summary>
        public string Rol { get; set; } = "Empleado";
    }
}
