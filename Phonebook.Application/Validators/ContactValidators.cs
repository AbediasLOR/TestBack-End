using FluentValidation;
using Phonebook.Application.DTOs;
using System.Linq;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
        RuleFor(x => x.Telefone).NotEmpty().WithMessage("Telefone é obrigatório");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Enderecos).NotNull().Must(x => x.Any())
            .WithMessage("Pelo menos um endereço é obrigatório");
    }
}

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Telefone).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Enderecos).NotNull().Must(x => x.Any());
    }
}
