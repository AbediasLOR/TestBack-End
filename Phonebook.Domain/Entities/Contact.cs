namespace Phonebook.Domain.Entities;

public class Address
{
    public string Logradouro { get; set; } = default!;
    public string Numero { get; set; } = default!;
    public string Cidade { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public string Cep { get; set; } = default!;
}

public class Contact
{
    public string Id { get; set; } = default!;
    public string Nome { get; set; } = default!;
    public string Telefone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime? DataNascimento { get; set; }
    public List<Address> Enderecos { get; set; } = new();
}
