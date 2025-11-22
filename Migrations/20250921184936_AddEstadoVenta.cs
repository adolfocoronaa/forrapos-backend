using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "ventas",
                type: "longtext",
                nullable: false,
                defaultValue: "Pendiente");

            migrationBuilder.Sql("UPDATE ventas SET Estado = 'Completado' WHERE Fecha < NOW() - INTERVAL 30 DAY;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "ventas");
        }
    }
}
