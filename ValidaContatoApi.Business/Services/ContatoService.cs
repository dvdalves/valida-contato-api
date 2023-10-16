using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Services
{
    public class ContatoService : IContatoService
    {
        public Task Adicionar(Contato contato)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(Contato contato)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contato>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
