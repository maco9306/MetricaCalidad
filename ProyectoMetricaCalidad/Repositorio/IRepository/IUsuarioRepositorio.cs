using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;

namespace AplicacionProyectoMetrica.Repositorio.IRepository
{
    public interface IUsuarioRepositorio
    {
        ICollection<AppUsuario> GetUsuarios();
        AppUsuario GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
