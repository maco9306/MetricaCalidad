using Microsoft.AspNetCore.Identity;

namespace AplicacionProyectoMetrica.Modelos
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
