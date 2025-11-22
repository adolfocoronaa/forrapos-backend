using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRazonSocialYRFCProveedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "razon_social",
                table: "Proveedor",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rfc",
                table: "Proveedor",
                type: "varchar(13)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "razon_social",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "rfc",
                table: "Proveedor");
        }
    }
}
