using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class InsertProductosIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Producto (nombre, descripcion, categoria, stock_actual, precio_actual, ImagenUrl)
                VALUES
                ('Alfalfa Premium', 'Alfalfa de alta calidad para ganado.', 'Alimentos', 100, 45.00, '/imagenes/productos/alfalfa.jpg'),
                ('Maíz Forrajero', 'Maíz de crecimiento rápido para forraje.', 'Semillas', 150, 38.50, '/imagenes/productos/maiz.jpg'),
                ('Trigo Integral', 'Trigo de calidad para animales.', 'Semillas', 120, 40.00, '/imagenes/productos/trigo.jpg'),
                ('Pasto Bermuda', 'Pasto para alimentación de ganado.', 'Alimentos', 90, 55.00, '/imagenes/productos/bermuda.jpg'),
                ('Sorgo Dulce', 'Sorgo nutritivo para forraje.', 'Semillas', 110, 37.00, '/imagenes/productos/sorgo.jpg'),
                ('Avena Forrajera', 'Avena ideal para el ganado.', 'Alimentos', 95, 50.00, '/imagenes/productos/avena.jpg'),
                ('Melaza Líquida', 'Complemento alimenticio para ganado.', 'Suplementos', 80, 60.00, '/imagenes/productos/melaza.jpg'),
                ('Sal Mineralizada', 'Sal para balance nutricional.', 'Suplementos', 200, 20.00, '/imagenes/productos/sal.jpg'),
                ('Zacate Rye Grass', 'Ideal para pastoreo.', 'Semillas', 150, 42.00, '/imagenes/productos/ryegrass.jpg'),
                ('Paja de Cebada', 'Paja de cebada para cama de ganado.', 'Alimentos', 130, 25.00, '/imagenes/productos/cebada.jpg'),
                ('Heno Timothy', 'Heno de alta calidad.', 'Alimentos', 70, 65.00, '/imagenes/productos/timothy.jpg'),
                ('Concentrado Bovino', 'Alimento balanceado para ganado bovino.', 'Suplementos', 180, 75.00, '/imagenes/productos/concentrado.jpg'),
                ('Maíz Rolado', 'Maíz tratado para digestión.', 'Alimentos', 120, 48.00, '/imagenes/productos/maiz-rolado.jpg'),
                ('Semilla de Girasol', 'Ideal para forraje y aceites.', 'Semillas', 140, 35.00, '/imagenes/productos/girasol.jpg'),
                ('Aditivo Probiótico', 'Mejora la digestión del ganado.', 'Suplementos', 90, 85.00, '/imagenes/productos/probiotico.jpg'),
                ('Zacate Kikuyo', 'Zacate resistente para pasto.', 'Semillas', 100, 45.00, '/imagenes/productos/kikuyo.jpg'),
                ('Cal Agrícola', 'Regula el pH del suelo.', 'Fertilizantes', 200, 15.00, '/imagenes/productos/cal.jpg'),
                ('Harina de Hueso', 'Suplemento de calcio.', 'Suplementos', 110, 28.00, '/imagenes/productos/hueso.jpg'),
                ('Fertilizante Orgánico', 'Aumenta la productividad de cultivos.', 'Fertilizantes', 150, 60.00, '/imagenes/productos/fertilizante.jpg'),
                ('Saco de Alimento Equino', 'Alimento balanceado para caballos.', 'Alimentos', 80, 90.00, '/imagenes/productos/equino.jpg')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Producto 
                WHERE nombre IN (
                    'Alfalfa Premium', 'Maíz Forrajero', 'Trigo Integral', 'Pasto Bermuda',
                    'Sorgo Dulce', 'Avena Forrajera', 'Melaza Líquida', 'Sal Mineralizada',
                    'Zacate Rye Grass', 'Paja de Cebada', 'Heno Timothy', 'Concentrado Bovino',
                    'Maíz Rolado', 'Semilla de Girasol', 'Aditivo Probiótico', 'Zacate Kikuyo',
                    'Cal Agrícola', 'Harina de Hueso', 'Fertilizante Orgánico', 'Saco de Alimento Equino'
                )
            ");
        }
    }
}
