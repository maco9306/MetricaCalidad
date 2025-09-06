namespace ProyectoMetricaCalidad.Dtos
{
    public class FacturaCrearDto
    {
        public int IdFactura { get; set; }
        public string NumFacServ { get; set; }
        public int IdServicio { get; set; }
        public int IdProducto { get; set; }
        public float PrecioTotal { get; set; }
        public int Descuento { get; set; }
        public DateTime? FechaFactura { get; set; }
        public int Cantidad { get; set; }
    }
}
