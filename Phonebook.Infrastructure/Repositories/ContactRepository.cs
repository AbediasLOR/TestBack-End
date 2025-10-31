using MongoDB.Bson;
using MongoDB.Driver;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Data;
using Phonebook.Infrastructure.Settings;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Phonebook.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly MongoContext _ctx;
    private readonly MongoSettings _settings;

    public ContactRepository(MongoContext ctx, MongoSettings settings)
    {
        _ctx = ctx;
        _settings = settings;
    }

    public async Task<Contact> CreateAsync(Contact c, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(c.Id))
            c.Id = ObjectId.GenerateNewId().ToString();
        await _ctx.Contacts(_settings).InsertOneAsync(c, cancellationToken: ct);
        return c;
    }

    public Task<List<Contact>> GetAllAsync(CancellationToken ct)
        => _ctx.Contacts(_settings).Find(Builders<Contact>.Filter.Empty).ToListAsync(ct);

    public async Task<Contact?> GetByIdAsync(string id, CancellationToken ct)
        => await _ctx.Contacts(_settings).Find(x => x.Id == id).FirstOrDefaultAsync(ct);

    public async Task<bool> UpdateAsync(Contact c, CancellationToken ct)
    {
        var result = await _ctx.Contacts(_settings).ReplaceOneAsync(x => x.Id == c.Id, c, cancellationToken: ct);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct)
    {
        var result = await _ctx.Contacts(_settings).DeleteOneAsync(x => x.Id == id, ct);
        return result.DeletedCount > 0;
    }
}
