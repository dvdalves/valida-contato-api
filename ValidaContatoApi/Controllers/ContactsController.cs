using Microsoft.AspNetCore.Mvc;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.ViewModels;

namespace ValidaContatoApi.Controllers;

public class ContactsController : BaseController
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contatoService)
    {
        _contactService = contatoService;
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] CreateContactVM contactViewModel)
    {
        return GetActionResult(await _contactService.Create(contactViewModel));
    }

    [HttpPut()]
    public async Task<IActionResult> Put([FromBody] UpdateContactVM contactViewModel)
    {
        return GetActionResult(await _contactService.Update(contactViewModel));
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        return GetActionResult(await _contactService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return GetActionResult(await _contactService.GetById(id));
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch([FromRoute] Guid id)
    {
        return GetActionResult(await _contactService.Toggle(id));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return GetActionResult(await _contactService.Delete(id));
    }
}
