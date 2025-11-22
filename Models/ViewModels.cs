// Este archivo iría en tu proyecto de ASP.NET Core
namespace Inventario.Models.ViewModels
{
    // Modelo para datos de series de tiempo (ej. ventas por mes)
    public class DatoTemporal
    {
        public string Periodo { get; set; } // 'Enero', 'Febrero', 'Lunes', 'Martes', etc.
        public decimal Total { get; set; }
    }

    // Modelo para datos agrupados (ej. compras por proveedor)
    public class DatoAgrupado
    {
        public string Etiqueta { get; set; } // Nombre del proveedor, nombre del producto
        public decimal Valor { get; set; } // Puede ser total en $ o cantidad
    }

    // Modelo para la pestaña de Ventas
    public class ReporteVentas
    {
        public List<DatoTemporal> VentasPorMes { get; set; }
        public List<DatoAgrupado> TopProductosVendidos { get; set; }
        public List<DatoAgrupado> TopClientes { get; set; }
    }

    // Modelo para la pestaña de Compras
    public class ReporteCompras
    {
        public List<DatoTemporal> ComprasPorMes { get; set; }
        public List<DatoAgrupado> TopProductosComprados { get; set; }
        public List<DatoAgrupado> TopProveedores { get; set; }
    }

    // Modelo para la pestaña de Finanzas (Revenue, Costos)
    public class ReporteFinanciero
    {
        public List<DatoTemporal> IngresosMensuales { get; set; }
        public List<DatoTemporal> CostosMensuales { get; set; }
        public List<DatoTemporal> GananciaMensual { get; set; }
    }
}
