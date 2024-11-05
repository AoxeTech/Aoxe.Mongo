namespace Aoxe.Mongo;

public static class AoxeMongoServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        string connectionString,
        string databaseName
    ) =>
        services.AddSingletonWithLazy<IAoxeMongoClient>(
            new AoxeMongoClient(connectionString, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<MongoUrl> mongoUrlFactory,
        string databaseName
    ) =>
        services.AddSingletonWithLazy<IAoxeMongoClient>(
            new AoxeMongoClient(mongoUrlFactory, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoUrl mongoUrl,
        string databaseName
    ) =>
        services.AddSingletonWithLazy<IAoxeMongoClient>(
            new AoxeMongoClient(mongoUrl, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<MongoClientSettings> mongoClientSettingsFactory,
        string databaseName
    ) =>
        services.AddSingletonWithLazy<IAoxeMongoClient>(
            new AoxeMongoClient(mongoClientSettingsFactory, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoClientSettings mongoClientSettings,
        string databaseName
    ) =>
        services.AddSingletonWithLazy<IAoxeMongoClient>(
            new AoxeMongoClient(mongoClientSettings, databaseName)
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        Func<AoxeMongoOptions> optionsFactory
    ) => services.AddSingletonWithLazy<IAoxeMongoClient>(new AoxeMongoClient(optionsFactory));

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        AoxeMongoOptions options
    ) => services.AddSingletonWithLazy<IAoxeMongoClient>(new AoxeMongoClient(options));
}
