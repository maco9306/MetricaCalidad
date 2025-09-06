using AplicacionProyectoMetrica.Data;
using AplicacionProyectoMetrica.Modelos;
using ProyectoMetricaCalidad.Repositorio.IRepository;

namespace ProyectoMetricaCalidad.Repositorio
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        public readonly ApplicationDbContext _context;

        public FacturaRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ActualizarFactura(Factura factura)
        {
            _context.Factura.Update(factura);
            return Guardar();
        }

        public bool BorrarFactura(Factura factura)
        {
            _context.Factura.Remove(factura);
            return Guardar();
        }

        public Factura BuscarFactura(int idFactura)
        {
            IQueryable<Factura> query = _context.Factura;

            if (idFactura > 0)
            {
                query = query.Where(e => e.IdFactura == idFactura);
            }
            return query.FirstOrDefault();
        }

        public bool CrearFactura(Factura factura)
        {
            _context.Factura.Add(factura);
            return Guardar();
        }

        public bool ExisteFactura(int id)
        {
            return _context.Factura.Any(c => c.IdFactura == id);
        }

        public ICollection<Factura> GetFacturas()
        {
            return _context.Factura.OrderBy(c => c.IdFactura).ToList();
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
