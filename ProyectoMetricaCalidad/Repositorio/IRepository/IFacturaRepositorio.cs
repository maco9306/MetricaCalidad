using AplicacionProyectoMetrica.Modelos;

namespace ProyectoMetricaCalidad.Repositorio.IRepository
{
    public interface IFacturaRepositorio
    {
        bool ActualizarFactura(Factura factura);
        bool BorrarFactura(Factura factura);
        Factura BuscarFactura(int idFactura);
        bool CrearFactura(Factura factura);
        bool ExisteFactura(int id);
        ICollection<Factura> GetFacturas();
        bool Guardar();
    }
}
