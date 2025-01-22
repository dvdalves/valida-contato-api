using ValidaContatoApi.Business.Common;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.ViewModels;

namespace ValidaContatoApi.Business.Interface;

public interface IContactService
{
    Task<Result<ContactDTO>> GetById(Guid id);
    Task<Result<IEnumerable<ContactDTO>>> GetAll();
    Task<Result<ContactDTO>> Create(CreateContactVM contactViewModel);
    Task<Result<ContactDTO>> Update(UpdateContactVM contactViewModel);
    Task<Result<ContactDTO>> Toggle(Guid id);
    Task<Result<ContactDTO>> Delete(Guid id);
}
