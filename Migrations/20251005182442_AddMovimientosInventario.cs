using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimientosInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "MovimientosInventario",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn), // o SQL Server Identity
                ProductoId = table.Column<int>(nullable: false),
                Tipo = table.Column<string>(maxLength: 20, nullable: false),
                Cantidad = table.Column<int>(nullable: false),
                Fecha = table.Column<DateTime>(nullable: false),
                Observacion = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovimientosInventario", x => x.Id);
                table.ForeignKey(
                    name: "FK_MovimientosInventario_Productos_ProductoId",
                    column: x => x.ProductoId,
                    principalTable: "Productos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosInventario_ProductoId",
                table: "MovimientosInventario",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosInventario_Fecha",
                table: "MovimientosInventario",
                column: "Fecha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "MovimientosInventario");
        }
    }
}
