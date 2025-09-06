using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;
using AutoMapper;
using ProyectoMetricaCalidad.Dtos;

namespace AplicacionProyectoMetrica.MapperProyect
{
    public class MetricaMapper : Profile
    {
        public MetricaMapper()
        {
            CreateMap<AppUsuario, UsuarioDto>().ReverseMap();
            CreateMap<AppUsuario, UsuarioDatosDto>().ReverseMap();
            CreateMap<Factura, FacturaDto>().ReverseMap();
            CreateMap<Cargo, CargoDto>().ReverseMap();
        }
    }
}
