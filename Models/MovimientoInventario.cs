using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa un movimiento de inventario, ya sea una entrada o una salida de productos.
    /// Contiene información sobre el producto, la cantidad y la fecha del movimiento.
    /// </summary>
    public class MovimientoInventario
    {
        /// <summary>
        /// Identificador único del movimiento (clave primaria).
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        /// <summary>
        /// Cantidad de productos movidos (positiva para entradas, negativa para salidas).
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Fecha y hora del movimiento.
        /// </summary>
        /// 
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>
        /// Tipo de movimiento: "Entrada" o "Salida".
        /// </summary>
        [Required]
        public string Tipo { get; set; }

        /// <summary>
        /// Navegación hacia el producto asociado al movimiento.
        /// </summary>
        [ForeignKey(nameof(ProductoId))]
        public Producto? Producto { get; set; }
    }
}