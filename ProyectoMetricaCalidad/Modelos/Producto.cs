namespace AplicacionProyectoMetrica.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NomProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int? PrecioUnitario { get; set; }
        public int? Existencias { get; set; }
    }
}
