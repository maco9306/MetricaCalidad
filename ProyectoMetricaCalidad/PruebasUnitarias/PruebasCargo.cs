using AplicacionProyectoMetrica.Controllers;
using AplicacionProyectoMetrica.Dtos;
using AplicacionProyectoMetrica.Modelos;
using AplicacionProyectoMetrica.Repositorio.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AplicacionProyectoMetrica.PruebasUnitarias
{
    public class PruebasCargo
    {
        private readonly Mock<ICargoRepositorio> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CargoController _controller;

        public PruebasCargo()
        {
            _mockRepo = new Mock<ICargoRepositorio>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CargoController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetCargo_ReturnsOkResult_WithListOfCargoDto()
        {
            // Arrange
            var cargos = new List<Cargo>
        {
            new Cargo { id_cargos = 1, tipo_cargo = "Cargo1" },
            new Cargo { id_cargos = 2, tipo_cargo = "Cargo2" }
        };

            var cargosDto = new List<CargoDto>
        {
            new CargoDto { id_cargos = 1, tipo_cargo = "Cargo1" },
            new CargoDto { id_cargos = 2, tipo_cargo = "Cargo2" }
        };

            _mockRepo.Setup(repo => repo.GetCargos()).Returns(cargos);
            _mockMapper.Setup(mapper => mapper.Map<Cargo>(It.IsAny<Cargo>())).Returns((Cargo c) => new Cargo
            {
                id_cargos = c.id_cargos,
                tipo_cargo = c.tipo_cargo
            });

            // Act
            var result = _controller.GetCargo();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCargos = Assert.IsType<List<CargoDto>>(okResult.Value);
            Assert.Equal(cargosDto.Count, returnedCargos.Count);
        }

        [Fact]
        public void GetCargo_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCargo(It.IsAny<int>())).Returns((Cargo)null);

            // Act
            var result = _controller.GetCargo(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CrearCargo_WithValidCargo_ReturnsCreatedAtRoute()
        {
            // Arrange
            var cargoDto = new CargoDto { id_cargos = 1, tipo_cargo = "Cargo1" };
            var cargo = new Cargo { id_cargos = 1, tipo_cargo = "Cargo1" };

            _mockRepo.Setup(repo => repo.ExisteCargo(It.IsAny<int>())).Returns(false);
            _mockMapper.Setup(mapper => mapper.Map<Cargo>(It.IsAny<CargoDto>())).Returns(cargo);
            _mockRepo.Setup(repo => repo.CrearCargo(cargo)).Returns(true);

            // Act
            var result = _controller.CrearCargo(cargoDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetCargo", createdResult.RouteName);
        }

        [Fact]
        public void CrearCargo_WithExistingCargo_ReturnsBadRequest()
        {
            // Arrange
            var cargoDto = new CargoDto { id_cargos = 1, tipo_cargo = "Cargo1" };

            _mockRepo.Setup(repo => repo.ExisteCargo(It.IsAny<int>())).Returns(true);

            // Act
            var result = _controller.CrearCargo(cargoDto);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, badRequestResult.StatusCode);
        }

        [Fact]
        public void BorrarCargo_WithExistingCargo_ReturnsNoContent()
        {
            // Arrange
            var cargo = new Cargo { id_cargos = 1, tipo_cargo = "Cargo1" };

            _mockRepo.Setup(repo => repo.ExisteCargo(It.IsAny<int>())).Returns(true);
            _mockRepo.Setup(repo => repo.GetCargo(It.IsAny<int>())).Returns(cargo);
            _mockRepo.Setup(repo => repo.BorrarCargo(cargo)).Returns(true);

            // Act
            var result = _controller.BorrarCargo(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void BorrarCargo_WithNonExistingCargo_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.ExisteCargo(It.IsAny<int>())).Returns(false);

            // Act
            var result = _controller.BorrarCargo(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
