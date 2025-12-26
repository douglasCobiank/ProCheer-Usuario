using Moq;
using Usuario.Core;
using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;
using Xunit;

namespace Usuario.Tests
{
    public class UsuarioHandlerTests
    {
        private readonly Mock<IUsuarioService> _usuarioServiceMock;
        private readonly UsuarioHandler _handler;

        public UsuarioHandlerTests()
        {
            _usuarioServiceMock = new Mock<IUsuarioService>();

            _handler = new UsuarioHandler(
                _usuarioServiceMock.Object
            );
        }

        [Fact]
        public async Task BuscaUsuarioAsync_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioDto>
            {
                new UsuarioDto()
            };

            _usuarioServiceMock
                .Setup(s => s.GetUsuarioAsync())
                .ReturnsAsync(usuarios);

            // Act
            var result = await _handler.BuscaUsuarioAsync();

            // Assert
            Assert.Equal(usuarios, result);

            _usuarioServiceMock.Verify(
                s => s.GetUsuarioAsync(),
                Times.Once
            );
        }

        [Fact]
        public async Task BuscaUsuarioByIdAsync_DeveRetornarUsuario()
        {
            // Arrange
            int id = 1;
            var usuario = new UsuarioDto();

            _usuarioServiceMock
                .Setup(s => s.GetUsuarioByIdAsync(id))
                .ReturnsAsync(usuario);

            // Act
            var result = await _handler.BuscaUsuarioByIdAsync(id);

            // Assert
            Assert.Equal(usuario, result);

            _usuarioServiceMock.Verify(
                s => s.GetUsuarioByIdAsync(id),
                Times.Once
            );
        }

        [Fact]
        public async Task BuscaUsuarioByNomeAsync_DeveRetornarUsuario()
        {
            // Arrange
            string nome = "Douglas";
            var usuario = new UsuarioDto();

            _usuarioServiceMock
                .Setup(s => s.GetUsuarioByNomeAsync(nome))
                .ReturnsAsync(usuario);

            // Act
            var result = await _handler.BuscaUsuarioByNomeAsync(nome);

            // Assert
            Assert.Equal(usuario, result);

            _usuarioServiceMock.Verify(
                s => s.GetUsuarioByNomeAsync(nome),
                Times.Once
            );
        }

        [Fact]
        public async Task BuscaUsuarioByUserAsync_DeveRetornarUsuario()
        {
            // Arrange
            string login = "teste";
            string senha = "123";
            var usuario = new UsuarioDto();

            _usuarioServiceMock
                .Setup(s => s.GetUsuarioByLoginAsync(login, senha))
                .ReturnsAsync(usuario);

            // Act
            var result = await _handler.BuscaUsuarioByUserAsync(login, senha);

            // Assert
            Assert.Equal(usuario, result);

            _usuarioServiceMock.Verify(
                s => s.GetUsuarioByLoginAsync(login, senha),
                Times.Once
            );
        }

        [Fact]
        public async Task CadastraUsuarioHandler_DeveChamarService()
        {
            // Arrange
            var usuarioDto = new UsuarioDto();

            // Act
            await _handler.CadastraUsuarioHandler(usuarioDto);

            // Assert
            _usuarioServiceMock.Verify(
                s => s.AddUsuarioAsync(usuarioDto),
                Times.Once
            );
        }

        [Fact]
        public async Task DeletaUsuarioAsync_DeveChamarService()
        {
            // Arrange
            int id = 1;

            // Act
            await _handler.DeletaUsuarioAsync(id);

            // Assert
            _usuarioServiceMock.Verify(
                s => s.DeletaUsuarioAsync(id),
                Times.Once
            );
        }

        [Fact]
        public async Task EditarUsuarioAsync_DeveChamarService()
        {
            // Arrange
            int id = 1;
            var usuarioDto = new UsuarioDto();

            // Act
            await _handler.EditarUsuarioAsync(usuarioDto, id);

            // Assert
            _usuarioServiceMock.Verify(
                s => s.EditarUsuarioAsync(usuarioDto, id),
                Times.Once
            );
        }
    }
}
