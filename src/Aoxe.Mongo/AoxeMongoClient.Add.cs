namespace Aoxe.Mongo;

public partial class AoxeMongoClient
{
    public void Add<T>(T entity)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertOne(entity);
    }

    public async ValueTask AddAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        await MongoDatabase
            .GetCollection<T>(tableName, _collectionSettings)
            .InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public void AddRange<T>(IEnumerable<T> entities)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        MongoDatabase.GetCollection<T>(tableName, _collectionSettings).InsertMany(entities);
    }

    public async ValueTask AddRangeAsync<T>(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        await MongoDatabase
            .GetCollection<T>(tableName, _collectionSettings)
            .InsertManyAsync(entities, cancellationToken: cancellationToken);
    }
}
