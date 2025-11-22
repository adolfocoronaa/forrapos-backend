using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa una compra realizada a un proveedor.
    /// Contiene la fecha, proveedor asociado, monto total y sus detalles.
    /// </summary>
    public class Compra
    {
        /// <summary>
        /// Identificador único de la compra (clave primaria).
        /// </summary>
        [Key]
        [Column("id_compra")]  // 
        public int IdCompra { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la compra.
        /// </summary>
        [Required]
        [Column("fecha_movimiento")]  // 
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Identificador del proveedor relacionado con la compra (clave foránea).
        /// </summary>
        [Column("id_proveedor")]
        public int ProveedorId { get; set; }

        /// <summary>
        /// Folio de la compra
        /// </summary>
        [Column("Folio")]
        public string? Folio { get; set; }

        /// <summary>
        /// Objeto proveedor relacionado con esta compra.
        /// </summary>
        [ForeignKey("ProveedorId")]
        public Proveedor? Proveedor { get; set; }

        /// <summary>
        /// Total monetario de la compra.
        /// </summary>
        [Column("total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Colección de detalles asociados a esta compra (productos, cantidades, etc.).
        /// </summary>
        public ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
    }
}
