using Usuario.Core.DTOs;
using Usuario.Core.Interfaces;

namespace Usuario.Core;

public class UsuarioHandler(IUsuarioService usuarioService) : IUsuarioHandler
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    public async Task<List<UsuarioDto>> BuscaUsuarioAsync()
    {
        return await _usuarioService.GetUsuarioAsync();
    }

    public async Task<UsuarioDto?> BuscaUsuarioByIdAsync(int id)
    {
        return await _usuarioService.GetUsuarioByIdAsync(id);
    }

    public async Task<UsuarioDto?> BuscaUsuarioByNomeAsync(string nome)
    {
        return await _usuarioService.GetUsuarioByNomeAsync(nome);
    }

    public async Task<UsuarioDto?> BuscaUsuarioByUserAsync(string usuario, string senha)
    {
        return await _usuarioService.GetUsuarioByLoginAsync(usuario, senha);
    }

    public async Task CadastraUsuarioHandler(UsuarioDto usuarioDto)
    {
        await _usuarioService.AddUsuarioAsync(usuarioDto);
    }

    public async Task DeletaUsuarioAsync(int id)
    {
        await _usuarioService.DeletaUsuarioAsync(id);
    }

    public async Task EditarUsuarioAsync(UsuarioDto user, int id)
    {
        await _usuarioService.EditarUsuarioAsync(user, id);
    }
}
