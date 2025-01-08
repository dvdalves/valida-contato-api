using Microsoft.AspNetCore.Mvc;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.ViewModels;

namespace ValidaContatoApi.Controllers
{
    public class ContactsController : BaseController
    {
        private readonly IContactService _contatoService;

        public ContactsController(IContactService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpPost("AdicionarContato")]
        public async Task<IActionResult> AdicionarContato([FromBody] CreateContactVM contatoViewModel)
        {
            return ObterIActionResult(await _contatoService.Adicionar(contatoViewModel));
        }

        [HttpPut("AtualizarContato")]
        public async Task<IActionResult> AtualizarContato([FromBody] UpdateContactVM contatoViewModel)
        {
            return ObterIActionResult(await _contatoService.Atualizar(contatoViewModel));
        }

        [HttpGet("ObterTodosContatos")]
        public async Task<IActionResult> ObterTodosContatos()
        {
            return ObterIActionResult(await _contatoService.ObterTodos());
        }

        [HttpGet("ObterContatoPorId/{id:guid}")]
        public async Task<IActionResult> ObterContatoPorId([FromRoute] Guid id)
        {
            return ObterIActionResult(await _contatoService.ObterPorId(id));
        }

        [HttpPatch("AtivarDesativarContato/{id:guid}")]
        public async Task<IActionResult> AtivarContato([FromRoute] Guid id)
        {
            return ObterIActionResult(await _contatoService.Ativar(id));
        }

        [HttpDelete("ExcluirContato/{id:guid}")]
        public async Task<IActionResult> RemoverContato([FromRoute] Guid id)
        {
            return ObterIActionResult(await _contatoService.Remover(id));
        }
    }
}
