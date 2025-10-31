namespace Phonebook.Infrastructure.Settings;

public class MongoSettings
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public string ContactsCollectionName { get; set; } = "contacts";
}
