using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.Resultados;
using ValidaContatoApi.Business.ViewModels;

namespace ValidaContatoApi.Business.Interface
{
    public interface IContatoService
    {
        Task<Resultado<ContatoDTO>> ObterPorId(Guid id);
        Task<Resultado<IEnumerable<ContatoDTO>>> ObterTodos();
        Task<Resultado<ContatoDTO>> Adicionar(CriarContatoVM contatoViewModel);
        Task<Resultado<ContatoDTO>> Atualizar(AtualizarContatoVM contatoViewModel);
        Task<Resultado<ContatoDTO>> Ativar(Guid id);
        Task<Resultado<ContatoDTO>> Remover(Guid id);
    }
}
