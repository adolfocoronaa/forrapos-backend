using System;
using System.Collections.Generic;

public class DashboardAlertDTO
{
    public string Name { get; set; } = "";
    public int Stock { get; set; }
}

public class RecentSaleDTO
{
    public int VentaId { get; set; }
    public string Cliente { get; set; } = "";
    public string ProductosResumen { get; set; } = "";
    public decimal Total { get; set; }
    public string Estado { get; set; } = "";
    public DateTime Fecha { get; set; }
}

public class EmployeeDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Rol { get; set; } = "";
    public bool IsActive { get; set; }
}

public class DashboardDTO
{
    public decimal VentasHoy { get; set; }
    public decimal VentasSemana { get; set; }
    public int ItemsVendidosHoy { get; set; }
    public int OrdenesActivas { get; set; }
    public List<DashboardAlertDTO> Alertas { get; set; } = new();
    public List<RecentSaleDTO> RecentSales { get; set; } = new();
    public List<EmployeeDTO> ActiveEmployees { get; set; } = new();
}