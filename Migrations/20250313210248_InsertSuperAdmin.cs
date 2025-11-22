using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class InsertSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                INSERT INTO Usuarios (Name, Email, Password, Rol)
                VALUES ('Admin', 'admin@example.com', '{BCrypt.Net.BCrypt.HashPassword("123456")}', 'Administrador')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Usuarios WHERE Email = 'admin@example.com'");
        }
    }
}
