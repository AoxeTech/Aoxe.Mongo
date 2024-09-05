namespace Aoxe.Mongo;

public static class AoxeMongoServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        string connectionString,
        string databaseName
    ) =>
        services.AddSingleton<IAoxeMongoClient>(
            new AoxeMongoClient(connectionString, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoUrl mongoUrl,
        string databaseName
    ) => services.AddSingleton<IAoxeMongoClient>(new AoxeMongoClient(mongoUrl, databaseName));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoClientSettings mongoClientSettings,
        string databaseName
    ) =>
        services.AddSingleton<IAoxeMongoClient>(
            new AoxeMongoClient(mongoClientSettings, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        AoxeMongoOptions options
    ) => services.AddSingleton<IAoxeMongoClient>(new AoxeMongoClient(options));
}
