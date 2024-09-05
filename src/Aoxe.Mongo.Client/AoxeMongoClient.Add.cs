namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient
{
    public void Add<T>(T entity)
        where T : class =>
        MongoDatabase
            .GetCollection<T>(GetTableName(typeof(T)), _collectionSettings)
            .InsertOne(entity);

    public async ValueTask AddAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class =>
        await MongoDatabase
            .GetCollection<T>(GetTableName(typeof(T)), _collectionSettings)
            .InsertOneAsync(entity, cancellationToken: cancellationToken);

    public void AddRange<T>(IEnumerable<T> entities)
        where T : class =>
        MongoDatabase
            .GetCollection<T>(GetTableName(typeof(T)), _collectionSettings)
            .InsertMany(entities);

    public async ValueTask AddRangeAsync<T>(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default
    )
        where T : class =>
        await MongoDatabase
            .GetCollection<T>(GetTableName(typeof(T)), _collectionSettings)
            .InsertManyAsync(entities, cancellationToken: cancellationToken);
}
