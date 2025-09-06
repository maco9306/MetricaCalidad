using AplicacionProyectoMetrica.Data;
using AplicacionProyectoMetrica.Modelos;
using AplicacionProyectoMetrica.Repositorio.IRepository;

namespace AplicacionProyectoMetrica.Repositorio
{
    public class CargoRepositorio : ICargoRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CargoRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCargo(Cargo cargo)
        {
            _bd.Cargos.Update(cargo);
            return Guardar();
        }

        public bool BorrarCargo(Cargo cargo)
        {
            _bd.Cargos.Remove(cargo);
            return Guardar();
        }

        public ICollection<Cargo> BuscarCargo(string nombre)
        {
            IQueryable<Cargo> query = _bd.Cargos;

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.tipo_cargo.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearCargo(Cargo cargo)
        {
            _bd.Cargos.Add(cargo);
            return Guardar();
        }

        public bool ExisteCargo(int id)
        {
            return _bd.Cargos.Any(c => c.id_cargos == id);
        }

        public Cargo GetCargo(int cargoId)
        {
            return _bd.Cargos.FirstOrDefault(c => c.id_cargos == cargoId);
        }

        public ICollection<Cargo> GetCargos()
        {
            return _bd.Cargos.OrderBy(c => c.tipo_cargo).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
