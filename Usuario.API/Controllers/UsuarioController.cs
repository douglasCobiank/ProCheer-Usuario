using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Usuario.API.Models;
using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;

namespace Usuario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioHandler usuarioHandler, IMapper mapper) : ControllerBase
    {
        private readonly IUsuarioHandler _usuarioHandler = usuarioHandler;
        private readonly IMapper _mapper = mapper;

        [HttpPost("cadastrar")]
        public IActionResult CriarUsuario([FromBody] Users usuarioDto)
        {
            var usuario = _mapper.Map<UsuarioDto>(usuarioDto);
            _usuarioHandler.CadastraUsuarioHandler(usuario);

            return Ok(new[] { $"Usuario criado" });
        }

        [HttpPost("editar")]
        public IActionResult EditarUsuario([FromBody] Users usuarioDto, int id)
        {
            var usuario = _mapper.Map<UsuarioDto>(usuarioDto);
            _usuarioHandler.EditarUsuarioAsync(usuario, id);

            return Ok(new[] { $"Usuario editado" });
        }

        [HttpPost("deletar/{id}")]
        public IActionResult DeletarUsuario(int id)
        {
            _usuarioHandler.DeletaUsuarioAsync(id);

            return Ok(new[] { $"Usuario deletado" });
        }

        [HttpPost("buscar-usuarios")]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var response = await _usuarioHandler.BuscaUsuarioAsync();

            return Ok(new[] { response});
        }

        [HttpPost("buscar-usuario-login")]
        public async Task<IActionResult> BuscarUsuarioPorLogin(string login, string senha)
        {
            var response = await _usuarioHandler.BuscaUsuarioByUserAsync(login, senha);

            return Ok(new[] { response });
        }

        [HttpPost("buscar-usuario-id/{id}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var response = await _usuarioHandler.BuscaUsuarioByIdAsync(id);

            return Ok(new[] { response });
        }
        
        [HttpPost("buscar-usuario-nome/{nome}")]
        public async Task<IActionResult> BuscarUsuarioPorNome(string nome)
        {
            var response = await _usuarioHandler.BuscaUsuarioByNomeAsync(nome);

            return Ok(new[] { response});
        }
    }
}