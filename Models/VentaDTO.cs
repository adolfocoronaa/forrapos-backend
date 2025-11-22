public class VentaDTO
{
    public DateTime? Fecha { get; set; } // se puede editar si lo deseas

    public string MetodoPago { get; set; } = string.Empty;

    public string? Cliente { get; set; }

    public string? RFC { get; set; }

    public string? DireccionFiscal { get; set; }

    public string? CorreoFactura { get; set; }

    public string? UsoCFDI { get; set; }

    public string? RazonSocial { get; set; }
    public List<DetalleVentaDTO> Detalles { get; set; } = new();
}

public class DetalleVentaDTO
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}