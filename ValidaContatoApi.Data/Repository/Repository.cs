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

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<TEntity> ObterPorId(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> ObterTodos()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Remover(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}
