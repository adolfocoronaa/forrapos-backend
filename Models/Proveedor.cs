using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    [Table("proveedor")] 
    public class Proveedor
    {
        [Key]
        [Column("id_proveedor")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Column("nombre")]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        [Column("contacto")]
        public string? Contacto { get; set; }

        [MaxLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        [Column("email")]
        public string? Email { get; set; }

        [MaxLength(200)]
        [Column("direccion")]
        public string? Direccion { get; set; }

        [MaxLength(150)]
        [Column("razon_social")]
        public string? RazonSocial { get; set; }

        [MaxLength(13)]
        [Column("rfc")]
        public string? Rfc { get; set; }
    }
}
