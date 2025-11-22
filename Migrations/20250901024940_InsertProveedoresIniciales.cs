using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class InsertProveedoresIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Proveedor (nombre, razon_social, rfc, contacto, telefono, email, direccion)
                VALUES
                ('AgroAlimentos SA', 'AgroAlimentos Sociedad Anónima', 'AAA010101AAA', 'Juan Pérez', '4421234567', 'contacto@agroalimentos.com', 'Av. Principal #123, Querétaro'),
                ('Forrajes del Norte', 'Forrajes del Norte S.A. de C.V.', 'FDN020202BBB', 'María López', '4429876543', 'ventas@forrajesnorte.com', 'Calle Secundaria 45, Querétaro'),
                ('Semillas y Más', 'Semillas y Más S.A.', 'SYM030303CCC', 'Pedro Gómez', '4421122334', 'info@semillasyemas.com', 'Blvd. Agricola 78, Querétaro'),
                ('Alimentos Equinos', 'Alimentos Equinos S.A. de C.V.', 'AEQ040404DDD', 'Laura Martínez', '4422233445', 'ventas@alimentosequinos.com', 'Camino Real 56, Querétaro'),
                ('Suplementos Ganaderos', 'Suplementos Ganaderos S.A.', 'SGG050505EEE', 'Carlos Torres', '4423344556', 'contacto@suplementosganaderos.com', 'Calle del Sol 12, Querétaro')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.Sql(@"
                DELETE FROM Proveedor 
                WHERE nombre IN (
                    'AgroAlimentos SA', 
                    'Forrajes del Norte', 
                    'Semillas y Más', 
                    'Alimentos Equinos', 
                    'Suplementos Ganaderos'
                )
            ");
        }
    }
}
