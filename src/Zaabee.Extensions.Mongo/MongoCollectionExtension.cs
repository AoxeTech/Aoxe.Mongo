namespace Zaabee.Extensions.Mongo;

public static class MongoCollectionExtension
{
    public static UpdateResult UpdateMany<T>(this IMongoCollection<T> mongoCollection,
        Expression<Func<T, bool>> where, Expression<Func<T>> update, UpdateOptions? options = null,
        CancellationToken cancellationToken = default) where T : class
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (where is null) throw new ArgumentNullException(nameof(where));

        var updateDefinition = new UpdateExpressionVisitor<T>().GetUpdateDefinition(update);
        return mongoCollection.UpdateMany(where, updateDefinition, options, cancellationToken);
    }

    public static async ValueTask<UpdateResult> UpdateManyAsync<T>(this IMongoCollection<T> mongoCollection,
        Expression<Func<T, bool>> where, Expression<Func<T>> update, UpdateOptions? options = null,
        CancellationToken cancellationToken = default) where T : class
    {
        if (update is null) throw new ArgumentNullException(nameof(update));
        if (where is null) throw new ArgumentNullException(nameof(where));

        var updateDefinition = new UpdateExpressionVisitor<T>().GetUpdateDefinition(update);
        return await mongoCollection.UpdateManyAsync(where, updateDefinition, options, cancellationToken);
    }
}