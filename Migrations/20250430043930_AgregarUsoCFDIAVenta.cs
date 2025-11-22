using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AgregarUsoCFDIAVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsoCFDI",
                table: "ventas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsoCFDI",
                table: "ventas");
        }
    }
}
