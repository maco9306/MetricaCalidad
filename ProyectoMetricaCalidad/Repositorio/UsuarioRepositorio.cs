using AplicacionProyectoMetrica.Data;
using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;
using AplicacionProyectoMetrica.Repositorio.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AplicacionProyectoMetrica.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private string? claveSecreta;
        private readonly UserManager<AppUsuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;


        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config,
            UserManager<AppUsuario> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _bd = bd;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            _roleManager = roleManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public AppUsuario GetUsuario(string usuarioId)
        {
            return _bd.AppUsuario.FirstOrDefault(u => u.Id == usuarioId);
        }

        public ICollection<AppUsuario> GetUsuarios()
        {
            return _bd.AppUsuario.OrderBy(u => u.UserName).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuariobd = _bd.AppUsuario.FirstOrDefault(u => u.UserName == usuario);
            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            AppUsuario usuario = new AppUsuario()
            {
                UserName = usuarioRegistroDto.NombreUsuario,
                Email = usuarioRegistroDto.NombreUsuario,
                NormalizedEmail = usuarioRegistroDto.NombreUsuario.ToUpper(),
                Nombre = usuarioRegistroDto.Nombre
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRegistroDto.Password);
            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("registrado"));
                }

                await _userManager.AddToRoleAsync(usuario, "registrado");
                var usuarioRetornado = _bd.AppUsuario.FirstOrDefault(u => u.UserName == usuarioRegistroDto.NombreUsuario);
                return _mapper.Map<UsuarioDatosDto>(usuarioRetornado);
            }

            return new UsuarioDatosDto();

        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            try
            {
                var usuario = _bd.AppUsuario.FirstOrDefault(
                    u => u.UserName.ToLower() == usuarioLoginDto.NombreUsuario.ToLower());

                bool isValida = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);

                if (usuario == null || isValida == false)
                {
                    return new UsuarioLoginRespuestaDto()
                    {
                        Token = "",
                        Usuario = null
                    };
                }

                var roles = await _userManager.GetRolesAsync(usuario);

                var manejadorToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(claveSecreta);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = manejadorToken.CreateToken(tokenDescriptor);

                UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
                {
                    Token = manejadorToken.WriteToken(token),
                    Usuario = _mapper.Map<UsuarioDatosDto>(usuario)
                };

                return usuarioLoginRespuestaDto;

            }
            catch (Exception ex)
            {
                return new UsuarioLoginRespuestaDto();
            }
        }
    }
}
