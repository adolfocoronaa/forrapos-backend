using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFolioYMetodoPagoAVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Folio",
                table: "ventas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MetodoPago",
                table: "ventas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Folio",
                table: "ventas");

            migrationBuilder.DropColumn(
                name: "MetodoPago",
                table: "ventas");
        }
    }
}
