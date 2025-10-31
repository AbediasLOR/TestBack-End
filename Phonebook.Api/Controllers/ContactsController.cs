using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Contacts;
using Phonebook.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Api.Controllers;

[ApiController]
[Route("contatos")]
public class ContactsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ContactResponse>> Create([FromBody] CreateContactRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateContactCommand(request), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactResponse>>> GetAll(CancellationToken ct)
    {
        var result = await mediator.Send(new GetContactsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContactResponse>> GetById(string id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetContactByIdQuery(id), ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, [FromBody] UpdateContactRequest request, CancellationToken ct)
    {
        var ok = await mediator.Send(new UpdateContactCommand(id, request), ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartialUpdate(string id, [FromBody] UpdateContactRequest request, CancellationToken ct)
    {
        var ok = await mediator.Send(new UpdateContactCommand(id, request), ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteContactCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
