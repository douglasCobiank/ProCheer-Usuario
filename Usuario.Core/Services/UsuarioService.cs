using AutoMapper;
using Microsoft.Extensions.Logging;
using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;
using Usuario.Infrastructure.Data.Models;
using Usuario.Infrastructure.Repositories;

namespace Usuario.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddUsuarioAsync(UsuarioDto usuarioDto)
        {
            try
            {
                var userData = _mapper.Map<UsuarioData>(usuarioDto);
                await _usuarioRepository.AddAsync(userData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar usuário {@Usuario}", usuarioDto);
                throw;
            }
        }

        public async Task DeletaUsuarioAsync(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);

                if (usuario is null)
                    return;

                await _usuarioRepository.DeleteAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar usuário com Id {UsuarioId}", id);
                throw;
            }
        }

        public async Task EditarUsuarioAsync(UsuarioDto usuarioDto, int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);

                if (usuario is null)
                    return;

                usuario.Nome = usuarioDto.Nome;
                usuario.Email = usuarioDto.Email;
                usuario.Login = usuarioDto.Login;
                usuario.SenhaHash = usuarioDto.SenhaHash;
                usuario.Telefone = usuarioDto.Telefone;
                usuario.TipoUsuario = usuarioDto.TipoUsuario;

                await _usuarioRepository.EditAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar usuário com Id {UsuarioId}", id);
                throw;
            }
        }

        public async Task<List<UsuarioDto>> GetUsuarioAsync()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllWithIncludeAsync();
                return _mapper.Map<List<UsuarioDto>>(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários");
                throw;
            }
        }

        public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                return usuario is null ? null : _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por Id {UsuarioId}", id);
                throw;
            }
        }

        public async Task<UsuarioDto?> GetUsuarioByLoginAsync(string login, string senha)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByUsuarioeSenhaAsync(login, senha);
                return usuario is null ? null : _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por login {Login}", login);
                throw;
            }
        }

        public async Task<UsuarioDto?> GetUsuarioByNomeAsync(string nome)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetByNomeAsync(nome);
                return usuarios is null ? null : _mapper.Map<UsuarioDto>(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por nome {Nome}", nome);
                throw;
            }
        }
    }
}
