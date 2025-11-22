using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenUrlToProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar nueva columna 'ImagenUrl' para productos
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Producto",
                type: "nvarchar(255)",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar la columna si se revierte la migración
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Producto"
            );
        }
    }
}
