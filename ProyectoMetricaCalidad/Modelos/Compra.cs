using Microsoft.Identity.Client;

namespace AplicacionProyectoMetrica.Modelos
{
    public class Compra
    {
        public int IdCompraCons { get; set; }
        public string IdCompra { get; set; }
        public long DocProveedor { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaCompra { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public decimal ImpuestoIva { get; set; }
        public double PrecioTotal { get; set; }
        public Producto Producto { get; set; }
    }
}
