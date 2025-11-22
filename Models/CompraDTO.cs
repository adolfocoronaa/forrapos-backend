public class CompraDTO
{
    public DateTime? Fecha { get; set; }
    public int? ProveedorId { get; set; }

    public List<DetalleCompraDTO> Detalles { get; set; } = new();
}
