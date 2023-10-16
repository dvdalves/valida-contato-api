using System.Linq.Expressions;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> ObterPorId(int id);
        Task<List<TEntity>> ObterTodos();
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(int id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
