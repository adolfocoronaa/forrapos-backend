using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa un detalle individual dentro de una compra.
    /// Incluye información del producto, cantidad, precios e impuestos aplicables.
    /// </summary>
    [Table("detallecompra")]
    public class DetalleCompra
    {
        /// <summary>
        /// Identificador único del detalle de compra (clave primaria).
        /// </summary>
        [Key]
        [Column("id_detalle_compra")]
        public int Id { get; set; }

        /// <summary>
        /// Clave foránea que relaciona el detalle con una compra específica.
        /// </summary>
        [Column("id_compra")]
        public int IdCompra { get; set; }

        /// <summary>
        /// Objeto de navegación hacia la entidad Compra.
        /// </summary>
        [ForeignKey("IdCompra")]
        public Compra Compra { get; set; }

        /// <summary>
        /// Clave foránea del producto adquirido.
        /// </summary>
        [Column("id_producto")]
        public int ProductoId { get; set; }

        /// <summary>
        /// Objeto de navegación hacia el producto.
        /// </summary>
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        /// <summary>
        /// Cantidad de productos adquiridos en esta línea de detalle.
        /// </summary>
        [Column("cantidad")]
        public int Cantidad { get; set; }

        /// <summary>
        /// Precio unitario del producto en el momento de la compra.
        /// </summary>
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Subtotal calculado como Cantidad * PrecioUnitario.
        /// </summary>
        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Valor del IVA aplicado a esta línea de detalle.
        /// </summary>
        [Column("iva")]
        public decimal IVA { get; set; }
    }
}
