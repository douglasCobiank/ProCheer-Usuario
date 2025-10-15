using AutoMapper;
using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;
using Usuario.Infrastructure.Data.Models;
using Usuario.Infrastructure.Repositories;

namespace Usuario.Core.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IMapper _mapper = mapper;
        public async Task AddUsuarioAsync(UsuarioDto usuarioDto)
        {
            var userData = _mapper.Map<UsuarioData>(usuarioDto);

            await _usuarioRepository.AddAsync(userData);
        }

        public async Task DeletaUsuarioAsync(int id)
        {
            var usuarioResult = _usuarioRepository.GetByIdAsync(id);

            if (usuarioResult is not null)
                await _usuarioRepository.DeleteAsync(usuarioResult.Result);
        }

        public async Task EditarUsuarioAsync(UsuarioDto user, int id)
        {
            var usuarioResult = _usuarioRepository.GetByIdAsync(id).Result;

            if (usuarioResult is not null)
            {
                usuarioResult.Nome = user.Nome;
                usuarioResult.Email = user.Email;
                usuarioResult.Login = user.Login;

                usuarioResult.SenhaHash = user.SenhaHash;

                usuarioResult.Telefone = user.Telefone;
                usuarioResult.TipoUsuario = user.TipoUsuario;

                await _usuarioRepository.EditAsync(usuarioResult);
            }
        }

        public async Task<List<UsuarioDto>> GetUsuarioAsync()
        {
            var atletasData = await _usuarioRepository.GetAllWithIncludeAsync();
            return _mapper.Map<List<UsuarioDto>>(atletasData);
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
        {
            var atletaData = _usuarioRepository.GetByIdAsync(id);
            return atletaData is null ? null : _mapper.Map<UsuarioDto>(atletaData.Result);
        }

        public async Task<UsuarioDto?> GetUsuarioByLoginAsync(string usuario, string senha)
        {
            var atletaData = _usuarioRepository.GetByUsuarioeSenhaAsync(usuario, senha);
            return atletaData is null ? null : _mapper.Map<UsuarioDto>(atletaData.Result);
        }

        public async Task<UsuarioDto?> GetUsuarioByNomeAsync(string nome)
        {
            var atletaData = _usuarioRepository.GetByNomeAsync(nome);
            return atletaData is null ? null : _mapper.Map<UsuarioDto>(atletaData.Result);
        }
    }
}