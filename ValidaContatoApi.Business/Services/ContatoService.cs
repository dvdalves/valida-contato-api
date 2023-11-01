using AutoMapper;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.Resultados;
using ValidaContatoApi.Business.Validations;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;
        private readonly ContatoValidation _validacao;

        public ContatoService(IContatoRepository contatoRepository, IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
            _validacao = new ContatoValidation();
        }

        public async Task<Resultado<ContatoDTO>> Adicionar(CriarContatoVM contatoViewModel)
        {
            var resultado = new Resultado<ContatoDTO>();
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

                var contato = _mapper.Map<Contato>(contatoViewModel);
                var contatoAdicionado = await _contatoRepository.Adicionar(contato);

                contatoAdicionado.Idade = _validacao.CalcularIdade(contato.DataNascimento);

                resultado.ResultadoOk("Contato adicionado com sucesso!");
                resultado.Result = _mapper.Map<ContatoDTO>(contatoAdicionado);
            }
            catch (Exception ex)
            {
                resultado.ResultadoErro(500, ex.Message);
            }

            return resultado;
        }

        public async Task<Resultado<ContatoDTO>> Atualizar(AtualizarContatoVM contatoViewModel)
        {
            var resultado = new Resultado<ContatoDTO>();
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

                _mapper.Map<AtualizarContatoVM, Contato>(contatoViewModel, contatoExiste);
                await _contatoRepository.SaveChanges();

                resultado.ResultadoOk("Contato alterado com sucesso!");
                resultado.Result = _mapper.Map<ContatoDTO>(contatoExiste);
            }
            catch (Exception ex)
            {
                resultado.ResultadoErro(500, ex.Message);
            }

            return resultado;
        }

        public async Task<Resultado<ContatoDTO>> ObterPorId(Guid id)
        {
            var resultado = new Resultado<ContatoDTO>();
            var contato = await _contatoRepository.ObterPorId(id);

            if (contato is null || !contato.Status)
            {
                resultado.ResultadoErro(204, "Contato não encontrado!");
                return resultado;
            }

            contato.Idade = _validacao.CalcularIdade(contato.DataNascimento);
            resultado.Result = _mapper.Map<ContatoDTO>(contato);
            resultado.ResultadoOk("Contato obtido com sucesso!");

            return resultado;
        }

        public async Task<Resultado<IEnumerable<ContatoDTO>>> ObterTodos()
        {
            var resultado = new Resultado<IEnumerable<ContatoDTO>>();

            var contatos = await _contatoRepository.Buscar(c => c.Status);

            if (!contatos?.Any() ?? true)
            {
                resultado.ResultadoErro(204, "Nenhum registro encontrado!");
            }
            else
            {
                foreach (var contato in contatos)
                {
                    contato.Idade = _validacao.CalcularIdade(contato.DataNascimento);
                }

                resultado.ResultadoOk("Sucesso");
                resultado.Result = _mapper.Map<IEnumerable<ContatoDTO>>(contatos);
            }

            return resultado;
        }

        public async Task<Resultado<ContatoDTO>> Remover(Guid id)
        {
            var resultado = new Resultado<ContatoDTO>();
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

        public async Task<Resultado<ContatoDTO>> Ativar(Guid id)
        {
            var resultado = new Resultado<ContatoDTO>();
            var contato = await _contatoRepository.ObterPorId(id);

            if (contato == null)
            {
                resultado.ResultadoErro(404, "Contato não encontrado!");
                return resultado;
            }

            contato.Status = !contato.Status;
            await _contatoRepository.Atualizar(contato);

            resultado.ResultadoOk(contato.Status ? "Contato ativado com sucesso!" : "Contato desativado com sucesso!");
            resultado.Result = _mapper.Map<ContatoDTO>(contato);

            return resultado;
        }
    }
}
