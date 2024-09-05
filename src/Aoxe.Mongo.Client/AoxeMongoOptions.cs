namespace Aoxe.Mongo.Client;

public class AoxeMongoOptions
{
    public AoxeMongoOptions(string connectionString, string database)
    {
        ConnectionString = connectionString;
        MongoUrl = new MongoUrl(connectionString);
        MongoClientSettings = MongoClientSettings.FromConnectionString(ConnectionString);
        Database = database;
    }

    public AoxeMongoOptions(MongoUrl mongoUrl, string database)
    {
        MongoUrl = mongoUrl;
        ConnectionString = MongoUrl.ToConnectionString();
        MongoClientSettings = MongoClientSettings.FromUrl(MongoUrl);
        Database = database;
    }

    public AoxeMongoOptions(MongoClientSettings mongoClientSettings, string database)
    {
        MongoClientSettings = mongoClientSettings;
        ConnectionString = MongoClientSettings.ToConnectionString();
        MongoUrl = new MongoUrl(ConnectionString);
        Database = database;
    }

    public string ConnectionString { get; protected set; }
    public MongoUrl MongoUrl { get; protected set; }
    public MongoClientSettings MongoClientSettings { get; protected set; }
    public string Database { get; protected set; }
    public MongoCollectionSettings MongoCollectionSettings { get; set; } =
        new() { AssignIdOnInsert = true };
    public DateTimeKind? MongoDateTimeKind { get; set; } = DateTimeKind.Local;
    public GuidRepresentation? MongoGuidRepresentation { get; set; } =
        GuidRepresentation.CSharpLegacy;
}
