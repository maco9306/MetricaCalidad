namespace AplicacionProyectoMetrica.Modelos
{
    public class Proveedor
    {
        public long DocumentoNIT { get; set; }
        public string NomProveedor { get; set; }
        public string ApellidoSociedad { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public int? IdTipoProveedor { get; set; }
        public TipoProveedor TipoProveedor { get; set; }
    }
}
