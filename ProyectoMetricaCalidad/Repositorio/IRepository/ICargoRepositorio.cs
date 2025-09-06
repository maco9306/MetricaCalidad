using AplicacionProyectoMetrica.Modelos;

namespace AplicacionProyectoMetrica.Repositorio.IRepository
{
    public interface ICargoRepositorio
    {
        bool ActualizarCargo(Cargo cargo);
        bool BorrarCargo(Cargo cargo);
        ICollection<Cargo> BuscarCargo(string nombre);
        bool CrearCargo(Cargo cargo);
        Cargo GetCargo(int cargoId);
        ICollection<Cargo> GetCargos();
        bool ExisteCargo(int id);
        bool Guardar();
    }
}
