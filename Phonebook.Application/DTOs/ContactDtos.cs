namespace Phonebook.Application.DTOs;

public record AddressDto(string Logradouro, string Numero, string Cidade, string Estado, string Cep);

public record CreateContactRequest(
    string Nome,
    string Telefone,
    string Email,
    DateTime? DataNascimento,
    IEnumerable<AddressDto> Enderecos
);

public record UpdateContactRequest(
    string Nome,
    string Telefone,
    string Email,
    DateTime? DataNascimento,
    IEnumerable<AddressDto> Enderecos
);

public record ContactResponse(
    string Id,
    string Nome,
    string Telefone,
    string Email,
    DateTime? DataNascimento,
    IEnumerable<AddressDto> Enderecos
);
