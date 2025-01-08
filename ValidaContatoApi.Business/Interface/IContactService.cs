using ValidaContatoApi.Business.Common;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.ViewModels;

namespace ValidaContatoApi.Business.Interface
{
    public interface IContactService
    {
        Task<Result<ContactDTO>> ObterPorId(Guid id);
        Task<Result<IEnumerable<ContactDTO>>> ObterTodos();
        Task<Result<ContactDTO>> Adicionar(CreateContactVM contatoViewModel);
        Task<Result<ContactDTO>> Atualizar(UpdateContactVM contatoViewModel);
        Task<Result<ContactDTO>> Ativar(Guid id);
        Task<Result<ContactDTO>> Remover(Guid id);
    }
}
