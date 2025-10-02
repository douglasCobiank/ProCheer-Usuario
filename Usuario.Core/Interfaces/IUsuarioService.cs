using Usuario.Core.DTOs;

namespace Usuario.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task AddUsuarioAsync(UsuarioDto user);
        Task<List<UsuarioDto>> GetUsuarioAsync();
        Task<UsuarioDto?> GetUsuarioByIdAsync(string usuario, string senha);
        Task EditarUsuarioAsync(UsuarioDto user, int id);
        Task DeletaUsuarioAsync(int id);
    }
}