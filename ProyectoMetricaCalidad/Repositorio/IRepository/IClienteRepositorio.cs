using AplicacionProyectoMetrica.Modelos;

namespace ProyectoMetricaCalidad.Repositorio.IRepository
{
    public interface IClienteRepositorio
    {
        bool ActualizarCliente(Cliente cargo);
        bool BorrarCliente(Cliente cargo);
        ICollection<Cliente> BuscarCliente(string nombre);
        bool CrearCliente(Cliente cargo);
        Cliente GetCliente(int cargoId);
        ICollection<Cliente> GetClientes();
        bool ExisteCliente(int id);
    }
}
