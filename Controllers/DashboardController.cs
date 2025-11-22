// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data; // ajusta namespace
using System.Linq;
using Inventario.Models;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context) {
        _context = context;
    }

    [HttpGet("estadisticas")]
    public async Task<IActionResult> GetEstadisticas() {
        var hoy = DateTime.Today;
        var semanaInicio = hoy.AddDays(-(int)hoy.DayOfWeek);

        var ventasHoy = await _context.Ventas
            .Where(v => v.Fecha.Date == hoy)
            .SumAsync(v => (decimal?)v.Total) ?? 0;

        var ventasSemana = await _context.Ventas
            .Where(v => v.Fecha >= semanaInicio && v.Fecha <= hoy.AddDays(1).AddTicks(-1))
            .SumAsync(v => (decimal?)v.Total) ?? 0;

        var itemsVendidosHoy = await _context.DetallesVenta
            .Where(d => d.Venta.Fecha.Date == hoy)
            .SumAsync(d => (int?)d.Cantidad) ?? 0;

        // Cambié la condición para contar órdenes NO completadas como "activas"
        var ordenesActivas = await _context.Ventas
            .Where(v => v.Fecha.Date == hoy && v.Estado != "Completado")
            .CountAsync();

        var alertas = await _context.Productos
            .Where(p => p.Stock <= 5)
            .Select(p => new DashboardAlertDTO { Name = p.Name, Stock = p.Stock })
            .ToListAsync();

        var recentSales = await _context.Ventas
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto) // asegurar que Producto esté cargado
            .OrderByDescending(v => v.Fecha)
            .Take(10)
            .Select(v => new RecentSaleDTO {
                VentaId = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                Cliente = string.IsNullOrEmpty(v.Cliente) ? "Walk-in Customer" : v.Cliente,
                ProductosResumen = string.Join(", ", v.Detalles.Select(d => d.Producto.Name + " (x" + d.Cantidad + ")")),
                Estado = v.Estado ?? "Pendiente"
            }).ToListAsync();

        // Si tienes tabla Empleados, trae los activos:
        var activeEmployees = new List<EmployeeDTO>();
        if (_context.Set<Usuario>().Any()) {
            activeEmployees = await _context.Usuarios
                .Where(e => e.IsActive)
                .Select(e => new EmployeeDTO { Id = e.Id, Nombre = e.Name, Rol = e.Rol, IsActive = e.IsActive })
                .ToListAsync();
        }

        var dto = new DashboardDTO {
            VentasHoy = ventasHoy,
            VentasSemana = ventasSemana,
            ItemsVendidosHoy = itemsVendidosHoy,
            OrdenesActivas = ordenesActivas,
            Alertas = alertas,
            RecentSales = recentSales,
            ActiveEmployees = activeEmployees
        };

        return Ok(dto);
    }
}
