using System.Linq.Expressions;
using Usuario.Infrastructure.Data.Models;

namespace Usuario.Infrastructure.Repositories
{
    public interface IUsuarioRepository
    {
        Task AddAsync(UsuarioData usuarioData);
        Task EditAsync(UsuarioData usuarioData);
        Task DeleteAsync(UsuarioData usuarioData);
        Task<UsuarioData?> GetByUsuarioeSenhaAsync(string usuario, string senha);
        Task<UsuarioData?> GetByIdAsync(int id);
        Task<IEnumerable<UsuarioData>> GetAllAsync();
        Task<IEnumerable<UsuarioData>> FindAsync(Expression<Func<UsuarioData, bool>> predicate);
        Task<List<UsuarioData>> GetAllWithIncludeAsync();
    }
}