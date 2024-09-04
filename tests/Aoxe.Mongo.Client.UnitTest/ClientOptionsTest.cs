using MongoDB.Driver;

namespace Aoxe.Mongo.Client.UnitTest;

public class ClientOptionsTest
{
    private const string ConnectionString = "mongodb://localhost:27017";
    private const string DatabaseName = "Aoxe";
    private readonly MongoUrl _mongoUrl = new(ConnectionString);
    private readonly MongoClientSettings _mongoClientSettings =
        MongoClientSettings.FromConnectionString(ConnectionString);

    [Fact]
    public void AoxeClientOptionsConnectionStringTest()
    {
        var options = new AoxeMongoOptions(ConnectionString, DatabaseName);
        Assert.NotNull(options.ConnectionString);
        Assert.NotNull(options.MongoUrl);
        Assert.NotNull(options.MongoClientSettings);
        Assert.NotNull(options.Database);
    }

    [Fact]
    public void AoxeClientOptionsMongoUrlTest()
    {
        var options = new AoxeMongoOptions(_mongoUrl, DatabaseName);
        Assert.NotNull(options.ConnectionString);
        Assert.NotNull(options.MongoUrl);
        Assert.NotNull(options.MongoClientSettings);
        Assert.NotNull(options.Database);
    }

    [Fact]
    public void AoxeClientOptionsMongoClientSettingsTest()
    {
        var options = new AoxeMongoOptions(_mongoClientSettings, DatabaseName);
        Assert.NotNull(options.ConnectionString);
        Assert.NotNull(options.MongoUrl);
        Assert.NotNull(options.MongoClientSettings);
        Assert.NotNull(options.Database);
    }
}
