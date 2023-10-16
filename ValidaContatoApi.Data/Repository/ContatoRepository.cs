using System.Linq.Expressions;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Data.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        public Task Adicionar(Contato entity)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Contato entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contato>> Buscar(Expression<Func<Contato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Contato> ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Contato>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
