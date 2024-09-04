namespace Aoxe.Mongo.UnitTest;

public class DependencyInjectionTest
{
    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByConnectionString()
    {
        var option = new AoxeMongoOptions("mongodb://localhost:27017", "test");
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(option.ConnectionString, option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByMongoUrl()
    {
        var option = new AoxeMongoOptions("mongodb://localhost:27017", "test");
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(option.MongoUrl, option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }

    [Fact]
    public void AddAoxeMongo_ShouldRegisterAoxeMongoClientByMongoClientSettings()
    {
        var option = new AoxeMongoOptions("mongodb://localhost:27017", "test");
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAoxeMongo(option.MongoClientSettings, option.Database);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var aoxeMongoClient = serviceProvider.GetRequiredService<IAoxeMongoClient>();
        aoxeMongoClient.Should().NotBeNull();
    }
}
