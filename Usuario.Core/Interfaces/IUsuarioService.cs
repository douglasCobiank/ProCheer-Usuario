using Usuario.Core.DTOs;

namespace Usuario.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task AddUsuarioAsync(UsuarioDto user);
        Task<List<UsuarioDto>> GetUsuarioAsync();
        Task<UsuarioDto?> GetUsuarioByLoginAsync(string usuario, string senha);
        Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
        Task<UsuarioDto?> GetUsuarioByNomeAsync(string nome);
        Task EditarUsuarioAsync(UsuarioDto user, int id);
        Task DeletaUsuarioAsync(int id);
    }
}