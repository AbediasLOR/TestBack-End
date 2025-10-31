using Phonebook.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Infrastructure.Repositories;

public interface IContactRepository
{
    Task<Contact> CreateAsync(Contact c, CancellationToken ct);
    Task<List<Contact>> GetAllAsync(CancellationToken ct);
    Task<Contact?> GetByIdAsync(string id, CancellationToken ct);
    Task<bool> UpdateAsync(Contact c, CancellationToken ct);
    Task<bool> DeleteAsync(string id, CancellationToken ct);
}
