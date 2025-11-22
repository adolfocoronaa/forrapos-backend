using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class CreateProveedorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    contacto = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.id_proveedor);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Proveedor");
        }
    }
}
