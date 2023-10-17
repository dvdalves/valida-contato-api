using System.Linq.Expressions;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Interface
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
