using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task Adicionar(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }    

    public async Task<TEntity> ObterPorId(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> ObterTodos()
    {
        return await _dbSet.ToListAsync();
    }

    public Task Remover(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
