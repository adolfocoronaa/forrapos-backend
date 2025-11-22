using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovimientoInventarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimientoInventario",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                       .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    tipo_movimiento = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fecha_movimiento = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    referencia = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoInventario", x => x.id_movimiento);
                    table.ForeignKey(
                        name: "FK_MovimientoInventario_Producto",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoInventario_id_producto",
                table: "MovimientoInventario",
                column: "id_producto"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DropTable(name: "MovimientoInventario");
        }
    }
}
