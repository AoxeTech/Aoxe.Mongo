namespace Zaabee.Mongo;

public class ZaabeeMongoOptions
{
    private string _connectionString;

    public string ConnectionString
    {
        get => _connectionString;
        set
        {
            _connectionString = value;
            _mongoUrl = new MongoUrl(value);
            MongoClientSettings = MongoClientSettings.FromConnectionString(value);
        }
    }

    private MongoUrl _mongoUrl;

    public MongoUrl MongoUrl
    {
        get => _mongoUrl;
        set
        {
            _mongoUrl = value;
            _connectionString = _mongoUrl.ToString();
            MongoClientSettings = MongoClientSettings.FromUrl(value);
        }
    }

    public MongoClientSettings MongoClientSettings { get; set; }

    public string Database { get; set; }
    public DateTimeKind DateTimeKind { get; set; } = DateTimeKind.Local;
    public GuidRepresentation GuidRepresentation { get; set; } = GuidRepresentation.CSharpLegacy;
}
