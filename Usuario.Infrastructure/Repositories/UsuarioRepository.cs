using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Usuario.Infrastructure.Data;
using Usuario.Infrastructure.Data.Models;

namespace Usuario.Infrastructure.Repositories
{
    public class UsuarioRepository(CheerDbContext context) :  IUsuarioRepository
    {
        protected readonly CheerDbContext _context = context;
        protected readonly DbSet<UsuarioData> _dbSet = context.Set<UsuarioData>();

        public async Task AddAsync(UsuarioData usuarioData)
        {
            await _dbSet.AddAsync(usuarioData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsuarioData usuarioData)
        {
            if (_context.Entry(usuarioData).State == EntityState.Detached)
                _dbSet.Attach(usuarioData);

            _dbSet.Remove(usuarioData);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(UsuarioData usuarioData)
        {
            _context.Entry(usuarioData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioData>> FindAsync(Expression<Func<UsuarioData, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<UsuarioData>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<UsuarioData>> GetAllWithIncludeAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<UsuarioData?> GetByUsuarioeSenhaAsync(string usuario, string senha)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Login == usuario && a.SenhaHash == senha);
        }

        public async Task<UsuarioData?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.UsuarioId == id);
        }

        public async Task<UsuarioData?> GetByNomeAsync(string nome)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Nome == nome);
        }
    }
}