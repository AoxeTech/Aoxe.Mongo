﻿namespace Aoxe.Mongo;

public static class AoxeMongoServiceProviderExtensions
{
    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        string connectionString,
        string databaseName
    ) =>
        services.AddSingleton<IAoxeMongoClient>(
            new AoxeMongoClient(new AoxeMongoOptions(connectionString, databaseName))
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoUrl mongoUrl,
        string databaseName
    ) =>
        services.AddSingleton<IAoxeMongoClient>(
            new AoxeMongoClient(new AoxeMongoOptions(mongoUrl, databaseName))
        );

    public static IServiceCollection AddAoxeMongo(
        this IServiceCollection services,
        MongoClientSettings mongoClientSettings,
        string databaseName
    ) =>
        services.AddSingleton<IAoxeMongoClient>(
            new AoxeMongoClient(new AoxeMongoOptions(mongoClientSettings, databaseName))
        );
}
