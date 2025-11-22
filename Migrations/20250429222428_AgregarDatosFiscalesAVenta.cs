using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosFiscalesAVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "ventas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "ventas",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazonSocial",
                table: "ventas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DireccionFiscal",
                table: "ventas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Cliente", table: "ventas");
            migrationBuilder.DropColumn(name: "RFC", table: "ventas");
            migrationBuilder.DropColumn(name: "RazonSocial", table: "ventas");
            migrationBuilder.DropColumn(name: "DireccionFiscal", table: "ventas");
        }
    }
}
