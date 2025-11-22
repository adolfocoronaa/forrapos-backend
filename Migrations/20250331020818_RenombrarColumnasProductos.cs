using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarColumnasProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renombrar columnas de productos
        migrationBuilder.RenameColumn(
            name: "id_producto",
            table: "productos",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "nombre",
            table: "productos",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "precio_actual",
            table: "productos",
            newName: "Price");

        migrationBuilder.RenameColumn(
            name: "stock_actual",
            table: "productos",
            newName: "Stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir cambios en caso de rollback
        migrationBuilder.RenameColumn(
            name: "Id",
            table: "productos",
            newName: "id_producto");

        migrationBuilder.RenameColumn(
            name: "Name",
            table: "productos",
            newName: "nombre");

        migrationBuilder.RenameColumn(
            name: "Price",
            table: "productos",
            newName: "precio_actual");

        migrationBuilder.RenameColumn(
            name: "Stock",
            table: "productos",
            newName: "stock_actual");
        }
    }
}
