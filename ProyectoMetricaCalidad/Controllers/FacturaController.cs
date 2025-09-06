using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoMetricaCalidad.Dtos;
using ProyectoMetricaCalidad.Repositorio.IRepository;

namespace ProyectoMetricaCalidad.Controllers
{
    [Route("Factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaRepositorio _facturaRepositorio;
        private readonly IMapper _mapper;

        public FacturaController(IFacturaRepositorio facturaRepositorio, IMapper mapper)
        {
            _facturaRepositorio = facturaRepositorio;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFactura()
        {
            var listaFactura = _facturaRepositorio.GetFacturas();

            var listaFacturaDto = new List<FacturaDto>();

            foreach (var lista in listaFactura)
            {
                listaFacturaDto.Add(_mapper.Map<FacturaDto>(lista));
            }
            return Ok(listaFacturaDto);
        }

        [AllowAnonymous]
        [HttpGet("{facturaId:int}", Name = "BuscarFactura")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BuscarFactura(int facturaId)
        {
            var itemFactura = _facturaRepositorio.BuscarFactura(facturaId);

            if (itemFactura == null)
            {
                return NotFound();
            }

            var itemFacturaDto = _mapper.Map<Factura>(itemFactura);
            return Ok(itemFacturaDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CrearFactura")]
        [ProducesResponseType(201, Type = typeof(FacturaCrearDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearFactura([FromBody] FacturaCrearDto facturaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (facturaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_facturaRepositorio.ExisteFactura(facturaDto.IdFactura))
            {
                ModelState.AddModelError("", "La factura ya existe");
                return StatusCode(404, ModelState);
            }

            var factura = _mapper.Map<Factura>(facturaDto);

            if (!_facturaRepositorio.CrearFactura(factura))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{factura.IdFactura}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("BuscarFactura", new { FacturaId = factura.IdFactura }, factura);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{facturaId:int}", Name = "ActualizarPatchFactura")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchFactura(int facturaId, [FromBody] FacturaDto facturaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factura = _mapper.Map<Factura>(facturaDto);

            if (!_facturaRepositorio.ActualizarFactura(factura))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{factura.IdFactura}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{facturaId:int}", Name = "BorrarFactura")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarFactura(int facturaId)
        {
            if (!_facturaRepositorio.ExisteFactura(facturaId))
            {
                return NotFound();
            }

            var factura = _facturaRepositorio.BuscarFactura(facturaId);

            if (!_facturaRepositorio.BorrarFactura(factura))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro{factura.IdFactura}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
