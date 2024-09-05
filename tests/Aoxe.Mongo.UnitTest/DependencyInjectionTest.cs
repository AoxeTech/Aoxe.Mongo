namespace Aoxe.Mongo.UnitTest;

public class DependencyInjectionTest
{
    // Arrange
    private readonly AoxeMongoOptions _option =
        new("mongodb://localhost:27017", "test")
        {
            // set to null just for test because MongoClient can only register serializers with the same type once
            MongoGuidRepresentation = null,
            MongoDateTimeKind = null
        };

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByOptions()
    {
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(_option);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByConnectionString()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(_option.ConnectionString, _option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByMongoUrl()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(_option.MongoUrl, _option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByMongoClientSettings()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(_option.MongoClientSettings, _option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }
}
