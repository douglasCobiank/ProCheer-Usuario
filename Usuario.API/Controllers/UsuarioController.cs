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
        public async Task<IActionResult> CriarUsuario([FromBody] Users usuarioDto)
        {
            var usuario = _mapper.Map<UsuarioDto>(usuarioDto);
            await _usuarioHandler.CadastraUsuarioHandler(usuario);

            return NoContent();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> EditarUsuario([FromBody] Users usuarioDto, int id)
        {
            var usuario = _mapper.Map<UsuarioDto>(usuarioDto);
            await _usuarioHandler.EditarUsuarioAsync(usuario, id);

            return NoContent();
        }

        [HttpPost("deletar/{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            await _usuarioHandler.DeletaUsuarioAsync(id);

            return NoContent();
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