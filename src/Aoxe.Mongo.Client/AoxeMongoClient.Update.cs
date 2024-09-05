namespace Aoxe.Mongo.Client;

public partial class AoxeMongoClient
{
    public long Update<T>(T entity)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var filter = GetIdFilterDefinition(entity, _guidRepresentation);
        var result = collection.UpdateOne(
            filter,
            new BsonDocumentUpdateDefinition<T>(
                new BsonDocument { { "$set", entity.ToBsonDocument() } }
            )
        );
        return result.ModifiedCount;
    }

    public async ValueTask<long> UpdateAsync<T>(
        T entity,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var filter = GetIdFilterDefinition(entity, _guidRepresentation);
        var result = await collection.UpdateOneAsync(
            filter,
            new BsonDocumentUpdateDefinition<T>(
                new BsonDocument { { "$set", entity.ToBsonDocument() } }
            ),
            cancellationToken: cancellationToken
        );
        return result.ModifiedCount;
    }

    public long UpdateMany<T>(Expression<Func<T, bool>> where, Expression<Func<T>> update)
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var result = collection.UpdateMany(where, update);
        return result.ModifiedCount;
    }

    public async ValueTask<long> UpdateManyAsync<T>(
        Expression<Func<T, bool>> where,
        Expression<Func<T>> update,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        var tableName = GetTableName(typeof(T));
        var collection = MongoDatabase.GetCollection<T>(tableName, _collectionSettings);
        var result = await collection.UpdateManyAsync(
            where,
            update,
            cancellationToken: cancellationToken
        );
        return result.ModifiedCount;
    }
}
