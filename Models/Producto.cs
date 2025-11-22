using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa un producto en el inventario.
    /// Mapea la tabla 'productos' de la base de datos.
    /// </summary>
    public class Producto
    {
        /// <summary>
        /// Identificador único (PK). Coincide con la columna 'Id'.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto. Coincide con la columna 'Name'.
        /// </summary>
        [Required] 
        public string Name { get; set; }

        /// <summary>
        /// Descripción del producto. Coincide con la columna 'descripcion'.
        /// </summary>
        public string? descripcion { get; set; } // Corregido

        /// <summary>
        /// Categoría del producto. Coincide con la columna 'categoria'.
        /// </summary>
        public string? categoria { get; set; } // Corregido

        /// <summary>
        /// Cantidad en inventario. Coincide con la columna 'Stock'.
        /// </summary>
        public int Stock { get; set; }
        
        /// <summary>
        /// Precio unitario. Coincide con la columna 'Price'.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// URL de la imagen. Coincide con la columna 'ImagenUrl'.
        /// </summary>
        public string? ImagenUrl { get; set; }
    }
}