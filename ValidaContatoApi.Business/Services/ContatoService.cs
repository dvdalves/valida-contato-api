using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Data.Interface;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Business.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task Adicionar(Contato contato)
        {
            await _contatoRepository.Adicionar(contato);
        }

        public async Task Atualizar(Contato contato)
        {
            await _contatoRepository.Atualizar(contato);
        }       

        public async Task ObterPorId(Guid id)
        {
            await _contatoRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Contato>> ObterTodos()
        {
            return await _contatoRepository.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await _contatoRepository.Remover(id);
        }
        public void Dispose()
        {
            _contatoRepository?.Dispose();
        }
    }
}
