using AutoMapper;
using ValidaContatoApi.Business.Common;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Validations;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _contatoRepository;
    private readonly IMapper _mapper;
    private readonly ContactValidation _validacao;

    public ContactService(IContactRepository contatoRepository, IMapper mapper)
    {
        _contatoRepository = contatoRepository;
        _mapper = mapper;
        _validacao = new ContactValidation();
    }

    public async Task<Result<ContactDTO>> Create(CreateContactVM contactViewModel)
    {
        var resultado = new Result<ContactDTO>();
        try
        {
            if (!_validacao.ValidateDate(contactViewModel.BirthDate))
            {
                resultado.ErrorResult(400, "Cannot select a date greater than or equal to the current date!");
                return resultado;
            }

            if (!_validacao.ValidateIfOfLegalAge(contactViewModel.BirthDate))
            {
                resultado.ErrorResult(400, "Contact cannot be underage!");
                return resultado;
            }

            var contato = _mapper.Map<Contact>(contactViewModel);
            var contatoAdicionado = await _contatoRepository.Add(contato);

            contatoAdicionado.Age = _validacao.CalculateAge(contato.BirthDate);

            resultado.SuccessResult("Contact added successfully!");
            resultado.Data = _mapper.Map<ContactDTO>(contatoAdicionado);
        }
        catch (Exception ex)
        {
            resultado.ErrorResult(500, ex.Message);
        }

        return resultado;
    }

    public async Task<Result<ContactDTO>> Update(UpdateContactVM contactViewModel)
    {
        var resultado = new Result<ContactDTO>();
        try
        {
            var contatoExiste = await _contatoRepository.GetById(contactViewModel.Id);

            if (contatoExiste is null)
            {
                resultado.ErrorResult(404, "Contact does not exist!");
                return resultado;
            }

            if (!_validacao.ValidateDate(contactViewModel.BirthDate))
            {
                resultado.ErrorResult(400, "Cannot select a date greater than or equal to the current date!");
                return resultado;
            }

            if (!_validacao.ValidateIfOfLegalAge(contactViewModel.BirthDate))
            {
                resultado.ErrorResult(400, "Contact cannot be underage!");
                return resultado;
            }

            _mapper.Map<UpdateContactVM, Contact>(contactViewModel, contatoExiste);
            await _contatoRepository.SaveChanges();

            resultado.SuccessResult("Contact updated successfully!");
            resultado.Data = _mapper.Map<ContactDTO>(contatoExiste);
        }
        catch (Exception ex)
        {
            resultado.ErrorResult(500, ex.Message);
        }

        return resultado;
    }

    public async Task<Result<ContactDTO>> GetById(Guid id)
    {
        var resultado = new Result<ContactDTO>();
        var contato = await _contatoRepository.GetById(id);

        if (contato is null || !contato.Status)
        {
            resultado.ErrorResult(204, "Contact not found!");
            return resultado;
        }

        contato.Age = _validacao.CalculateAge(contato.BirthDate);
        resultado.Data = _mapper.Map<ContactDTO>(contato);
        resultado.SuccessResult("Contact retrieved successfully!");

        return resultado;
    }

    public async Task<Result<IEnumerable<ContactDTO>>> GetAll()
    {
        var resultado = new Result<IEnumerable<ContactDTO>>();

        var contatos = await _contatoRepository.Find(c => c.Status);

        if (!contatos?.Any() ?? true)
        {
            resultado.ErrorResult(204, "No records found!");
        }
        else
        {
            foreach (var contato in contatos!)
            {
                contato.Age = _validacao.CalculateAge(contato.BirthDate);
            }

            resultado.SuccessResult("Success");
            resultado.Data = _mapper.Map<IEnumerable<ContactDTO>>(contatos);
        }

        return resultado;
    }

    public async Task<Result<ContactDTO>> Delete(Guid id)
    {
        var resultado = new Result<ContactDTO>();
        var contato = await _contatoRepository.GetById(id);

        if (contato != null)
        {
            await _contatoRepository.Remove(id);
            resultado.SuccessResult("Contact deleted successfully!");
        }
        else
        {
            resultado.ErrorResult(404, "Contact not found!");
        }

        return resultado;
    }

    public async Task<Result<ContactDTO>> Toggle(Guid id)
    {
        var resultado = new Result<ContactDTO>();
        var contato = await _contatoRepository.GetById(id);

        if (contato == null)
        {
            resultado.ErrorResult(404, "Contact not found!");
            return resultado;
        }

        contato.Status = !contato.Status;
        await _contatoRepository.Update(contato);

        resultado.SuccessResult(contato.Status ? "Contact activated successfully!" : "Contact deactivated successfully!");
        resultado.Data = _mapper.Map<ContactDTO>(contato);

        return resultado;
    }
}
