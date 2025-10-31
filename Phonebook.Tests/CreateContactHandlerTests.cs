using Phonebook.Application.Contacts;
using Phonebook.Application.DTOs;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Repositories;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

public class CreateContactHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_And_Return_Response()
    {
        var repo = new Mock<IContactRepository>();
        repo.Setup(r => r.CreateAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Contact c, CancellationToken _) => { c.Id = "abc123"; return c; });

        var handler = new CreateContactHandler(repo.Object);
        var req = new CreateContactRequest("Ana","11","ana@ex.com", null,
            new [] { new AddressDto("Rua","1","C","E","000") });
        var res = await handler.Handle(new CreateContactCommand(req), default);

        Assert.Equal("abc123", res.Id);
        Assert.Equal("Ana", res.Nome);
    }
}
