using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    categoria = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    stock_actual = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    precio_actual = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.id_producto);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Producto");
        }
    }
}
