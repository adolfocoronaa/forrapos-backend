using Microsoft.EntityFrameworkCore;
using Inventario.Models;

namespace Inventario.Data
{
    /// <summary>
    /// Contexto de base de datos que configura las entidades y sus respectivas tablas.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe opciones para la configuración del DbContext.
        /// </summary>
        /// <param name="options">Opciones de configuración para DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Representa la tabla 'producto' en la base de datos.
        /// </summary>
        public DbSet<Producto> Productos { get; set; }

        /// <summary>
        /// Representa la tabla 'usuario' en la base de datos.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Representa la tabla 'venta' en la base de datos.
        /// </summary>
        public DbSet<Venta> Ventas { get; set; }

        /// <summary>
        /// Representa la tabla 'detalleventa' en la base de datos.
        /// </summary>
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        /// <summary>
        /// Representa la tabla 'compra' en la base de datos.
        /// </summary>
        public DbSet<Compra> Compras { get; set; }

        /// <summary>
        /// Representa la tabla 'detallecompra' en la base de datos.
        /// </summary>
        public DbSet<DetalleCompra> DetallesCompra { get; set; }

        /// <summary>
        /// Representa la tabla 'proveedor' en la base de datos.
        /// </summary>
        public DbSet<Proveedor> Proveedores { get; set; }

        /// <summary>
        /// Representa la tabla de movimientos de inventario.
        /// </summary>
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        /// <summary>
        /// Configuración de los modelos y nombres de las tablas
        /// </summary>
        /// <param name="modelBuilder">Builder de modelos de Entity Framework.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Establecer nombres correctos para las tablas en la base de datos
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Venta>().ToTable("ventas");
            modelBuilder.Entity<DetalleVenta>().ToTable("detalleventa");
            modelBuilder.Entity<Compra>().ToTable("compra");
            modelBuilder.Entity<DetalleCompra>().ToTable("detallecompra");
            modelBuilder.Entity<Proveedor>().ToTable("proveedor");
            modelBuilder.Entity<MovimientoInventario>().ToTable("movimientosinventario");

            base.OnModelCreating(modelBuilder);
        }
    }
}
