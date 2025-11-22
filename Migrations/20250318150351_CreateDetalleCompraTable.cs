using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class CreateDetalleCompraTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    id_detalle_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_compra = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.id_detalle_compra);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Compra",
                        column: x => x.id_compra,
                        principalTable: "Compra",
                        principalColumn: "id_compra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Producto",
                        column: x => x.id_producto,
                        principalTable: "Producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_id_compra",
                table: "DetalleCompra",
                column: "id_compra"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DetalleCompra");
        }
    }
}
