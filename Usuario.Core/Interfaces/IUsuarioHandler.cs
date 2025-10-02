using Usuario.Core.DTOs;

namespace Usuario.Core.Interfaces
{
    public interface IUsuarioHandler
    {
        Task CadastraUsuarioHandler(UsuarioDto usuarioDto);
        Task<List<UsuarioDto>> BuscaUsuarioAsync();
        Task<UsuarioDto?> BuscaUsuarioByUserAsync(string usuario, string senha);
        Task EditarUsuarioAsync(UsuarioDto user, int id);
        Task DeletaUsuarioAsync(int id);
    }
}