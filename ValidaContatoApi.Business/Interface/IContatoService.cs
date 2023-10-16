using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Interface
{
    public interface IContatoService : IDisposable
    {
        Task Adicionar(Contato contato);
        Task Atualizar(Contato contato);
        Task Remover(Guid id);
        Task ObterPorId(Guid id);
        Task<IEnumerable<Contato>> ObterTodos();
    }
}
