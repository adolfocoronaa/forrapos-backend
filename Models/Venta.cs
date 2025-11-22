using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    /// <summary>
    /// Representa una venta realizada en el sistema.
    /// Contiene información como fecha, total y los productos vendidos.
    /// </summary>
    public class Venta
    {
        /// <summary>
        /// Identificador único de la venta (clave primaria).
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Fecha en la que se realizó la venta.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Monto total de la venta.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Metodo de pago de la venta.
        /// </summary>
        public string MetodoPago { get; set; } = string.Empty;

        /// <summary>
        /// Folio o número de referencia de la venta
        /// </summary>
        public string Folio { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string? Cliente { get; set; }
        /// <summary>
        /// RFC del cliente
        /// </summary>
        public string? RFC { get; set; }
        /// <summary>
        /// Razon socila del cliente
        /// </summary>
        public string? RazonSocial { get; set; }
        /// <summary>
        /// Dirección fiscal del cliente
        /// </summary>
        public string? DireccionFiscal { get; set; }

        /// <summary>
        /// Dirección fiscal del cliente
        /// </summary>
        public string? CorreoFactura { get; set; }

        /// <summary>
        /// Dirección fiscal del cliente
        /// </summary>
        public string? UsoCFDI { get; set; }

        public string Estado { get; set; } = "Pendiente";

        /// <summary>
        /// Lista de detalles de productos vendidos en esta venta.
        /// </summary>
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
