using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Usuario.API.Controllers;
using Usuario.API.Models;
using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;

namespace Usuario.Tests
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioHandler> _usuarioHandlerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _usuarioHandlerMock = new Mock<IUsuarioHandler>();
            _mapperMock = new Mock<IMapper>();
            _controller = new UsuarioController(_usuarioHandlerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void CriarUsuario_RetornaOk()
        {
            // Arrange
            var user = new Users();
            var usuarioDto = new UsuarioDto();
            _mapperMock.Setup(m => m.Map<UsuarioDto>(user)).Returns(usuarioDto);

            // Act
            var result = _controller.CriarUsuario(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new[] { "Usuario criado" }, okResult.Value);
            _usuarioHandlerMock.Verify(h => h.CadastraUsuarioHandler(usuarioDto), Times.Once);
        }

        [Fact]
        public void EditarUsuario_RetornaOk()
        {
            // Arrange
            var user = new Users();
            var usuarioDto = new UsuarioDto();
            int id = 1;
            _mapperMock.Setup(m => m.Map<UsuarioDto>(user)).Returns(usuarioDto);

            // Act
            var result = _controller.EditarUsuario(user, id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new[] { "Usuario editado" }, okResult.Value);
            _usuarioHandlerMock.Verify(h => h.EditarUsuarioAsync(usuarioDto, id), Times.Once);
        }

        [Fact]
        public void DeletarUsuario_RetornaOk()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.DeletarUsuario(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new[] { "Usuario deletado" }, okResult.Value);
            _usuarioHandlerMock.Verify(h => h.DeletaUsuarioAsync(id), Times.Once);
        }

        [Fact]
        public async Task BuscarUsuarios_RetornaOkComUsuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioDto> { new UsuarioDto() };
            _usuarioHandlerMock.Setup(h => h.BuscaUsuarioAsync()).ReturnsAsync(usuarios);

            // Act
            var result = await _controller.BuscarUsuarios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new[] { usuarios }, okResult.Value);
        }

        [Fact]
        public async Task BuscarUsuarioPorLogin_RetornaOkComUsuario()
        {
            // Arrange
            string login = "teste";
            string senha = "123";
            var usuario = new UsuarioDto();
            _usuarioHandlerMock.Setup(h => h.BuscaUsuarioByUserAsync(login, senha)).ReturnsAsync(usuario);

            // Act
            var result = await _controller.BuscarUsuarioPorLogin(login, senha);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(new[] { usuario }, okResult.Value);
        }
    }
}