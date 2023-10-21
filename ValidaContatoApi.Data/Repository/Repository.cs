using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Adicionar(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> Atualizar(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Remover(Guid id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {

            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
