namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient
{
    public long Delete<T>(T entity)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var filter = GetJsonFilterDefinition(entity, _guidSerializer);
        var result = collection.DeleteOne(filter);
        return result.DeletedCount;
    }

    public async ValueTask<long> DeleteAsync<T>(
        T entity,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var filter = GetJsonFilterDefinition(entity, _guidSerializer);
        var result = await collection.DeleteOneAsync(filter, cancellationToken);
        return result.DeletedCount;
    }

    public long DeleteMany<T>(Expression<Func<T, bool>> where)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        return collection.DeleteMany(where).DeletedCount;
    }

    public async ValueTask<long> DeleteAsync<T>(
        Expression<Func<T, bool>> where,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var result = await collection.DeleteManyAsync(where, cancellationToken);
        return result.DeletedCount;
    }
}
