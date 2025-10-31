using MediatR;
using Phonebook.Application.DTOs;
using System.Collections.Generic;

namespace Phonebook.Application.Contacts;

// CREATE
public record CreateContactCommand(CreateContactRequest Request) : IRequest<ContactResponse>;

// GET ALL
public record GetContactsQuery() : IRequest<IEnumerable<ContactResponse>>;

// GET BY ID
public record GetContactByIdQuery(string Id) : IRequest<ContactResponse?>;

// UPDATE
public record UpdateContactCommand(string Id, UpdateContactRequest Request) : IRequest<bool>;

// DELETE
public record DeleteContactCommand(string Id) : IRequest<bool>;
