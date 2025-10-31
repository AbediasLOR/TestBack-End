using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Phonebook.Application.Contacts;

public class CreateContactHandler(IContactRepository repo) : IRequestHandler<CreateContactCommand, ContactResponse>
{
    public async Task<ContactResponse> Handle(CreateContactCommand cmd, CancellationToken ct)
    {
        var r = cmd.Request;
        var entity = new Contact
        {
            Nome = r.Nome,
            Telefone = r.Telefone,
            Email = r.Email,
            DataNascimento = r.DataNascimento,
            Enderecos = r.Enderecos.Select(a => new Address
            {
                Logradouro = a.Logradouro,
                Numero = a.Numero,
                Cidade = a.Cidade,
                Estado = a.Estado,
                Cep = a.Cep
            }).ToList()
        };
        var created = await repo.CreateAsync(entity, ct);
        return new ContactResponse(
            created.Id, created.Nome, created.Telefone, created.Email, created.DataNascimento,
            created.Enderecos.Select(e => new AddressDto(e.Logradouro, e.Numero, e.Cidade, e.Estado, e.Cep)));
    }
}

public class GetContactsHandler(IContactRepository repo) : IRequestHandler<GetContactsQuery, IEnumerable<ContactResponse>>
{
    public async Task<IEnumerable<ContactResponse>> Handle(GetContactsQuery request, CancellationToken ct)
    {
        var list = await repo.GetAllAsync(ct);
        return list.Select(c => new ContactResponse(
            c.Id, c.Nome, c.Telefone, c.Email, c.DataNascimento,
            c.Enderecos.Select(e => new AddressDto(e.Logradouro, e.Numero, e.Cidade, e.Estado, e.Cep))));
    }
}

public class GetContactByIdHandler(IContactRepository repo) : IRequestHandler<GetContactByIdQuery, ContactResponse?>
{
    public async Task<ContactResponse?> Handle(GetContactByIdQuery q, CancellationToken ct)
    {
        var c = await repo.GetByIdAsync(q.Id, ct);
        return c is null ? null : new ContactResponse(
            c.Id, c.Nome, c.Telefone, c.Email, c.DataNascimento,
            c.Enderecos.Select(e => new AddressDto(e.Logradouro, e.Numero, e.Cidade, e.Estado, e.Cep)));
    }
}

public class UpdateContactHandler(IContactRepository repo) : IRequestHandler<UpdateContactCommand, bool>
{
    public async Task<bool> Handle(UpdateContactCommand cmd, CancellationToken ct)
    {
        var r = cmd.Request;
        var existing = await repo.GetByIdAsync(cmd.Id, ct);
        if (existing is null) return false;

        existing.Nome = r.Nome;
        existing.Telefone = r.Telefone;
        existing.Email = r.Email;
        existing.DataNascimento = r.DataNascimento;
        existing.Enderecos = r.Enderecos.Select(a => new Address
        {
            Logradouro = a.Logradouro, Numero = a.Numero, Cidade = a.Cidade, Estado = a.Estado, Cep = a.Cep
        }).ToList();

        return await repo.UpdateAsync(existing, ct);
    }
}

public class DeleteContactHandler(IContactRepository repo) : IRequestHandler<DeleteContactCommand, bool>
{
    public Task<bool> Handle(DeleteContactCommand cmd, CancellationToken ct)
        => repo.DeleteAsync(cmd.Id, ct);
}
