using Phonebook.Application.DTOs;
using Phonebook.Application.Validators;
using Xunit;
using System;
using System.Collections.Generic;

public class ContactValidatorsTests
{
    [Fact]
    public void CreateValidator_Should_Fail_When_Missing_Required()
    {
        var validator = new CreateContactRequestValidator();
        var req = new CreateContactRequest("", "", "naoemail", null, new List<AddressDto>());
        var result = validator.Validate(req);
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count >= 1);
    }

    [Fact]
    public void CreateValidator_Should_Pass_With_Valid_Data()
    {
        var validator = new CreateContactRequestValidator();
        var req = new CreateContactRequest(
            "João", "1199999-9999", "joao@ex.com", DateTime.UtcNow,
            new [] { new AddressDto("Rua X","100","SP","SP","01000-000") }
        );
        var result = validator.Validate(req);
        Assert.True(result.IsValid);
    }
}
