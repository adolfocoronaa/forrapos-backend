using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa un detalle de producto vendido dentro de una venta.
    /// Incluye el producto, cantidad vendida, precio y subtotal.
    /// </summary>
    public class DetalleVenta
    {
        /// <summary>
        /// Identificador único del detalle de venta (clave primaria).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Clave foránea que indica a qué venta pertenece este detalle.
        /// </summary>
        public int VentaId { get; set; }

        /// <summary>
        /// Objeto de navegación hacia la entidad Venta.
        /// </summary>
        public Venta Venta { get; set; }

        /// <summary>
        /// Clave foránea del producto vendido.
        /// </summary>
        public int ProductoId { get; set; }

        /// <summary>
        /// Objeto de navegación hacia el producto vendido.
        /// </summary>
        public Producto Producto { get; set; }

        /// <summary>
        /// Cantidad del producto vendido.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio unitario del producto al momento de la venta.
        /// </summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Subtotal calculado como Cantidad * PrecioUnitario.
        /// </summary>
        public decimal Subtotal { get; set; }
    }
}
