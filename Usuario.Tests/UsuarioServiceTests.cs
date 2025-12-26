using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Usuario.Core.DTOs;
using Usuario.Core.Services;
using Usuario.Infrastructure.Data.Models;
using Usuario.Infrastructure.Repositories;
using Xunit;

namespace Usuario.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<UsuarioService>> _loggerMock;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UsuarioService>>();

            _service = new UsuarioService(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task AddUsuarioAsync_DeveAdicionarUsuario()
        {
            // Arrange
            var dto = new UsuarioDto();
            var data = new UsuarioData();

            _mapperMock
                .Setup(m => m.Map<UsuarioData>(dto))
                .Returns(data);

            // Act
            await _service.AddUsuarioAsync(dto);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(data), Times.Once);
        }

        [Fact]
        public async Task AddUsuarioAsync_QuandoErro_DeveLogarERepropagar()
        {
            // Arrange
            var dto = new UsuarioDto();
            var exception = new Exception("Erro");

            _mapperMock
                .Setup(m => m.Map<UsuarioData>(dto))
                .Throws(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddUsuarioAsync(dto));

            VerificarLogErro("Erro ao adicionar usuário");
        }

        [Fact]
        public async Task DeletaUsuarioAsync_UsuarioExiste_DeveDeletar()
        {
            // Arrange
            var usuario = new UsuarioData();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(usuario);

            // Act
            await _service.DeletaUsuarioAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task DeletaUsuarioAsync_UsuarioNaoExiste_NaoDeveDeletar()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((UsuarioData?)null);

            // Act
            await _service.DeletaUsuarioAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<UsuarioData>()), Times.Never);
        }

        [Fact]
        public async Task EditarUsuarioAsync_UsuarioExiste_DeveEditar()
        {
            // Arrange
            var dto = new UsuarioDto
            {
                Nome = "Douglas",
                Email = "email@email.com",
                Login = "douglas",
                SenhaHash = "hash",
                Telefone = "999",
                TipoUsuario = "1"
            };

            var usuario = new UsuarioData();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(usuario);

            // Act
            await _service.EditarUsuarioAsync(dto, 1);

            // Assert
            _repositoryMock.Verify(r => r.EditAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task EditarUsuarioAsync_UsuarioNaoExiste_NaoDeveEditar()
        {
            // Arrange
            var dto = new UsuarioDto();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((UsuarioData?)null);

            // Act
            await _service.EditarUsuarioAsync(dto, 1);

            // Assert
            _repositoryMock.Verify(r => r.EditAsync(It.IsAny<UsuarioData>()), Times.Never);
        }

        [Fact]
        public async Task GetUsuarioAsync_DeveRetornarListaMapeada()
        {
            // Arrange
            var usuariosData = new List<UsuarioData> { new UsuarioData() };
            var usuariosDto = new List<UsuarioDto> { new UsuarioDto() };

            _repositoryMock
                .Setup(r => r.GetAllWithIncludeAsync())
                .ReturnsAsync(usuariosData);

            _mapperMock
                .Setup(m => m.Map<List<UsuarioDto>>(usuariosData))
                .Returns(usuariosDto);

            // Act
            var result = await _service.GetUsuarioAsync();

            // Assert
            Assert.Equal(usuariosDto, result);
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_UsuarioExiste_DeveRetornarDto()
        {
            // Arrange
            var usuarioData = new UsuarioData();
            var usuarioDto = new UsuarioDto();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(usuarioData);

            _mapperMock
                .Setup(m => m.Map<UsuarioDto>(usuarioData))
                .Returns(usuarioDto);

            // Act
            var result = await _service.GetUsuarioByIdAsync(1);

            // Assert
            Assert.Equal(usuarioDto, result);
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_UsuarioNaoExiste_DeveRetornarNull()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((UsuarioData?)null);

            // Act
            var result = await _service.GetUsuarioByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUsuarioByLoginAsync_UsuarioExiste_DeveRetornarDto()
        {
            // Arrange
            var usuarioData = new UsuarioData();
            var usuarioDto = new UsuarioDto();

            _repositoryMock
                .Setup(r => r.GetByUsuarioeSenhaAsync("login", "senha"))
                .ReturnsAsync(usuarioData);

            _mapperMock
                .Setup(m => m.Map<UsuarioDto>(usuarioData))
                .Returns(usuarioDto);

            // Act
            var result = await _service.GetUsuarioByLoginAsync("login", "senha");

            // Assert
            Assert.Equal(usuarioDto, result);
        }

        [Fact]
        public async Task GetUsuarioByNomeAsync_UsuarioExiste_DeveRetornarDto()
        {
            // Arrange
            var usuarioData = new UsuarioData();
            var usuarioDto = new UsuarioDto();

            _repositoryMock
                .Setup(r => r.GetByNomeAsync("Douglas"))
                .ReturnsAsync(usuarioData);

            _mapperMock
                .Setup(m => m.Map<UsuarioDto>(usuarioData))
                .Returns(usuarioDto);

            // Act
            var result = await _service.GetUsuarioByNomeAsync("Douglas");

            // Assert
            Assert.Equal(usuarioDto, result);
        }

        // =========================
        // Helper para verificar logs
        // =========================
        private void VerificarLogErro(string mensagem)
        {
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) =>
                        v.ToString()!.Contains(mensagem)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once
            );
        }
    }
}
