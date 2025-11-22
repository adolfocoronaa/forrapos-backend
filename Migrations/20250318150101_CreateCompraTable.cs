using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompraTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_proveedor = table.Column<int>(type: "int", nullable: false),
                    fecha_movimiento = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.id_compra);
                    table.ForeignKey(
                        name: "FK_Compra_Proveedor",
                        column: x => x.id_proveedor,
                        principalTable: "Proveedor",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Cascade);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Compra_id_proveedor",
                table: "Compra",
                column: "id_proveedor"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Compra");
        }
    }
}
