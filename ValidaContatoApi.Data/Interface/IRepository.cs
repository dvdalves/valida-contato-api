using System.Linq.Expressions;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Interface;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task<TEntity> Update(TEntity entity);
    Task Remove(Guid id);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChanges();
}