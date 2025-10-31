using MongoDB.Driver;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.Settings;

namespace Phonebook.Infrastructure.Data;

public class MongoContext
{
    public IMongoDatabase Database { get; }

    public MongoContext(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Contact> Contacts(MongoSettings settings)
        => Database.GetCollection<Contact>(settings.ContactsCollectionName);
}
