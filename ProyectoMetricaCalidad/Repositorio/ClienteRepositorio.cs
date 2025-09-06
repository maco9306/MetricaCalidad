using AplicacionProyectoMetrica.Data;
using AplicacionProyectoMetrica.Modelos;

namespace ProyectoMetricaCalidad.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public ClienteRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            _bd.Cliente.Update(cliente);
            return Guardar();
        }

        public bool BorrarCliente(Cliente cliente)
        {
            _bd.Cliente.Remove(cliente);
            return Guardar();
        }

        public ICollection<Cliente> BuscarCliente(string nombre)
        {
            IQueryable<Cliente> query = _bd.Cliente;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.NomCliente.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearCliente(Cliente cliente)
        {
            _bd.Cliente.Add(cliente);
            return Guardar();
        }

        public bool ExisteCliente(int id)
        {
            return _bd.Cliente.Any(c => c.IdCliente == id);
        }

        public Cliente GetCliente(int clienteId)
        {
            return _bd.Cliente.FirstOrDefault(c => c.IdCliente == clienteId);
        }

        public ICollection<Cliente> GetClientes()
        {
            return _bd.Cliente.OrderBy(c => c.IdCliente).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
