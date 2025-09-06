namespace AplicacionProyectoMetrica.Modelos
{
    public class Empleado
    {
        public int DocumentoEmp { get; set; }
        public string NomEmpleado { get; set; }
        public string ApeEmpleado { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public int? IdCargos { get; set; }
        public Cargo Cargo { get; set; }
    }
}
