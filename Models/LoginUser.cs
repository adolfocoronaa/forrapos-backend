using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    /// <summary>
    /// Modelo de datos utilizado para el inicio de sesión de un usuario.
    /// Contiene las credenciales mínimas necesarias.
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// Nombre de usuario que se utilizará para autenticar.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Contraseña del usuario en texto plano (se validará contra el hash).
        /// </summary>
        public string Password { get; set; }
    }
}
