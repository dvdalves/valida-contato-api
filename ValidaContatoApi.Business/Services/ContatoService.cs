using AutoMapper;
using ValidaContatoApi.Business.Common;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Validations;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Services
{
    public class ContatoService : IContactService
    {
        private readonly IContactRepository _contatoRepository;
        private readonly IMapper _mapper;
        private readonly ContactValidation _validacao;

        public ContatoService(IContactRepository contatoRepository, IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
            _validacao = new ContactValidation();
        }

        public async Task<Result<ContactDTO>> Adicionar(CreateContactVM contatoViewModel)
        {
            var resultado = new Result<ContactDTO>();
            try
            {
                if (!_validacao.ValidarData(contatoViewModel.DataNascimento))
                {
                    resultado.ResultadoErro(400, "Não pode ser selecionada data maior ou igual a atual!");
                    return resultado;
                }

                if (!_validacao.ValidaSeMaiorDeIdade(contatoViewModel.DataNascimento))
                {
                    resultado.ResultadoErro(400, "Contato não pode ser menor de idade!");
                    return resultado;
                }

                var contato = _mapper.Map<Contact>(contatoViewModel);
                var contatoAdicionado = await _contatoRepository.Adicionar(contato);

                contatoAdicionado.Age = _validacao.CalcularIdade(contato.BirthDate);

                resultado.ResultadoOk("Contato adicionado com sucesso!");
                resultado.Results = _mapper.Map<ContactDTO>(contatoAdicionado);
            }
            catch (Exception ex)
            {
                resultado.ResultadoErro(500, ex.Message);
            }

            return resultado;
        }

        public async Task<Result<ContactDTO>> Atualizar(UpdateContactVM contatoViewModel)
        {
            var resultado = new Result<ContactDTO>();
            try
            {
                var contatoExiste = await _contatoRepository.ObterPorId(contatoViewModel.Id);

                if (contatoExiste is null)
                {
                    resultado.ResultadoErro(404, "Contato não existe!");
                    return resultado;
                }

                if (!_validacao.ValidarData(contatoViewModel.DataNascimento))
                {
                    resultado.ResultadoErro(400, "Não pode ser selecionada data maior ou igual que a atual!");
                    return resultado;
                }

                if (!_validacao.ValidaSeMaiorDeIdade(contatoViewModel.DataNascimento))
                {
                    resultado.ResultadoErro(400, "Contato não pode ser menor de idade!");
                    return resultado;
                }

                _mapper.Map<UpdateContactVM, Contact>(contatoViewModel, contatoExiste);
                await _contatoRepository.SaveChanges();

                resultado.ResultadoOk("Contato alterado com sucesso!");
                resultado.Results = _mapper.Map<ContactDTO>(contatoExiste);
            }
            catch (Exception ex)
            {
                resultado.ResultadoErro(500, ex.Message);
            }

            return resultado;
        }

        public async Task<Result<ContactDTO>> ObterPorId(Guid id)
        {
            var resultado = new Result<ContactDTO>();
            var contato = await _contatoRepository.ObterPorId(id);

            if (contato is null || !contato.Status)
            {
                resultado.ResultadoErro(204, "Contato não encontrado!");
                return resultado;
            }

            contato.Age = _validacao.CalcularIdade(contato.BirthDate);
            resultado.Results = _mapper.Map<ContactDTO>(contato);
            resultado.ResultadoOk("Contato obtido com sucesso!");

            return resultado;
        }

        public async Task<Result<IEnumerable<ContactDTO>>> ObterTodos()
        {
            var resultado = new Result<IEnumerable<ContactDTO>>();

            var contatos = await _contatoRepository.Buscar(c => c.Status);

            if (!contatos?.Any() ?? true)
            {
                resultado.ResultadoErro(204, "Nenhum registro encontrado!");
            }
            else
            {
                foreach (var contato in contatos)
                {
                    contato.Age = _validacao.CalcularIdade(contato.BirthDate);
                }

                resultado.ResultadoOk("Sucesso");
                resultado.Results = _mapper.Map<IEnumerable<ContactDTO>>(contatos);
            }

            return resultado;
        }

        public async Task<Result<ContactDTO>> Remover(Guid id)
        {
            var resultado = new Result<ContactDTO>();
            var contato = await _contatoRepository.ObterPorId(id);

            if (contato != null)
            {
                await _contatoRepository.Remover(id);
                resultado.ResultadoOk("Contato removido com sucesso!");
            }
            else
            {
                resultado.ResultadoErro(404, "Contato não encontrado!");
            }

            return resultado;
        }

        public async Task<Result<ContactDTO>> Ativar(Guid id)
        {
            var resultado = new Result<ContactDTO>();
            var contato = await _contatoRepository.ObterPorId(id);

            if (contato == null)
            {
                resultado.ResultadoErro(404, "Contato não encontrado!");
                return resultado;
            }

            contato.Status = !contato.Status;
            await _contatoRepository.Atualizar(contato);

            resultado.ResultadoOk(contato.Status ? "Contato ativado com sucesso!" : "Contato desativado com sucesso!");
            resultado.Results = _mapper.Map<ContactDTO>(contato);

            return resultado;
        }
    }
}
