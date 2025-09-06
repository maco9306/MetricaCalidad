using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;
using AplicacionProyectoMetrica.Repositorio.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoMetricaCalidad.Dtos;

namespace AplicacionProyectoMetrica.Controllers
{
    [Route("api/Cargos")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private IMapper _mapper;

        public CargoController(ICargoRepositorio cargoRepositorio, IMapper mapper)
        {
            _cargoRepositorio = cargoRepositorio;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCargo()
        {
            var listaCargos = _cargoRepositorio.GetCargos();

            var listaCargosDto = new List<CargoDto>();

            foreach (var lista in listaCargos)
            {
                listaCargosDto.Add(_mapper.Map<CargoDto>(lista));
            }
            return Ok(listaCargosDto);
        }

        [AllowAnonymous]
        [HttpGet("{cargoId:int}", Name = "GetCargo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCargo(int cargoId)
        {
            var itemCategoria = _cargoRepositorio.GetCargo(cargoId);

            if (itemCategoria == null)
            {
                return NotFound();
            }

            var itemCategoriaDto = _mapper.Map<CargoDto>(itemCategoria);
            return Ok(itemCategoriaDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CrearCargo")]
        [ProducesResponseType(201, Type = typeof(CargoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCargo([FromBody] CargoDto cargoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cargoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_cargoRepositorio.ExisteCargo(cargoDto.id_cargos))
            {
                ModelState.AddModelError("", "El cargo ya existe");
                return StatusCode(404, ModelState);
            }

            var cargo = _mapper.Map<Cargo>(cargoDto);

            if (!_cargoRepositorio.CrearCargo(cargo))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{cargo.tipo_cargo}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetCargo", new { CargoId = cargo.id_cargos }, cargo);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{cargoId:int}", Name = "ActualizarPatchCargo")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchCargo(int cargoId, [FromBody] CargoDto cargoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cargo = _mapper.Map<Cargo>(cargoDto);

            if (!_cargoRepositorio.ActualizarCargo(cargo))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{cargo.tipo_cargo}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{cargoId:int}", Name = "BorrarCargo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCargo(int cargoId)
        {
            if (!_cargoRepositorio.ExisteCargo(cargoId))
            {
                return NotFound();
            }

            var cargo = _cargoRepositorio.GetCargo(cargoId);

            if (!_cargoRepositorio.BorrarCargo(cargo))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro{cargo.tipo_cargo}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
