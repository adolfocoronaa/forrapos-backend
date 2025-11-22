using Microsoft.AspNetCore.Mvc;
using Inventario.Data; // Tu DbContext
using Inventario.Models.ViewModels; // Los ViewModels que acabamos de crear
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Inventario.Controllers
{
    [Route("api/estadisticas")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/estadisticas/ventas
        [HttpGet("ventas")]
        public async Task<ActionResult<ReporteVentas>> GetReporteVentas()
        {
            var yearActual = DateTime.UtcNow.Year;
            
            // 1. Ventas por Mes (Tu lógica existente - está perfecta)
            var ventasData = await _context.Ventas
                .Where(v => v.Fecha.Year == yearActual && v.Estado == "Completado")
                .GroupBy(v => v.Fecha.Month)
                .Select(g => new
                {
                    MesNumero = g.Key,
                    TotalVenta = g.Sum(v => v.Total)
                })
                .OrderBy(g => g.MesNumero)
                .ToListAsync();

            // PASO 2: Procesar en memoria (client-side) para formatear
            var ventasPorMes = ventasData
                .Select(d => new DatoTemporal
                {
                    Periodo = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.MesNumero),
                    Total = d.TotalVenta
                })
                .ToList();
                
            // 2. Top 5 Productos Vendidos (por cantidad)
            var topProductos = await _context.DetallesVenta
                .Include(dv => dv.Producto) 
                .GroupBy(dv => dv.Producto.Name)
                .Select(g => new DatoAgrupado
                {
                    Etiqueta = g.Key,
                    Valor = g.Sum(dv => dv.Cantidad)
                })
                .OrderByDescending(g => g.Valor)
                .Take(5)
                .ToListAsync();

            // (Aquí iría la lógica para TopClientes, similar)

            var reporte = new ReporteVentas
            {
                VentasPorMes = ventasPorMes,
                TopProductosVendidos = topProductos,
                TopClientes = new List<DatoAgrupado>() // Placeholder
            };

            return Ok(reporte);
        }

        // GET: api/estadisticas/compras
        [HttpGet("compras")]
        public async Task<ActionResult<ReporteCompras>> GetReporteCompras()
        {
            var yearActual = DateTime.UtcNow.Year;

            var comprasData = await _context.Compras
                .Where(c => c.Fecha.Year == yearActual)
                .GroupBy(c => c.Fecha.Month)
                .Select(g => new
                {
                    MesNumero = g.Key,
                    TotalCompra = g.Sum(c => c.Total)
                })
                .OrderBy(g => g.MesNumero) // Ordena por el número de mes
                .ToListAsync();

            // PASO 2: Procesar en memoria para formatear
            var comprasPorMes = comprasData
                .Select(d => new DatoTemporal
                {
                    Periodo = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(d.MesNumero),
                    Total = d.TotalCompra
                })
                .ToList();

            // (Lógica para TopProductosComprados y TopProveedores iría aquí)

            var reporte = new ReporteCompras
            {
                ComprasPorMes = comprasPorMes,
                TopProductosComprados = new List<DatoAgrupado>(), // Placeholder
                TopProveedores = new List<DatoAgrupado>() // Placeholder
            };

            return Ok(reporte);
        }
        
        // GET: api/estadisticas/finanzas
        [HttpGet("finanzas")]
        public async Task<ActionResult<ReporteFinanciero>> GetReporteFinanciero()
        {
            var yearActual = DateTime.UtcNow.Year;

            // 1. Ingresos (Ventas)
            var ingresos = await _context.Ventas
                .Where(v => v.Fecha.Year == yearActual && v.Estado == "Completado")
                .GroupBy(v => v.Fecha.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(v => v.Total) })
                .ToListAsync();

            // 2. Costos (Compras)
            var costos = await _context.Compras
                .Where(c => c.Fecha.Year == yearActual)
                .GroupBy(c => c.Fecha.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(c => c.Total) })
                .ToListAsync();

            // 3. Combinar datos y calcular ganancia
            var ingresosMensuales = new List<DatoTemporal>();
            var costosMensuales = new List<DatoTemporal>();
            var gananciaMensual = new List<DatoTemporal>();

            for (int i = 1; i <= 12; i++)
            {
                var nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                var ingresoMes = ingresos.FirstOrDefault(x => x.Mes == i)?.Total ?? 0;
                var costoMes = costos.FirstOrDefault(x => x.Mes == i)?.Total ?? 0;

                ingresosMensuales.Add(new DatoTemporal { Periodo = nombreMes, Total = ingresoMes });
                costosMensuales.Add(new DatoTemporal { Periodo = nombreMes, Total = costoMes });
                gananciaMensual.Add(new DatoTemporal { Periodo = nombreMes, Total = ingresoMes - costoMes });
            }

            var reporte = new ReporteFinanciero
            {
                IngresosMensuales = ingresosMensuales,
                CostosMensuales = costosMensuales,
                GananciaMensual = gananciaMensual
            };
            
            return Ok(reporte);
        }
    }
}
