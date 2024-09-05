namespace Aoxe.Mongo.Client;

public static class MongoCollectionExtensions
{
    public static UpdateResult UpdateMany<T>(
        this IMongoCollection<T> mongoCollection,
        Expression<Func<T, bool>> where,
        Expression<Func<T>> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default
    )
        where T : class =>
        mongoCollection.UpdateMany(
            where,
            new UpdateExpressionVisitor<T>().GetUpdateDefinition(update),
            options,
            cancellationToken
        );

    public static async ValueTask<UpdateResult> UpdateManyAsync<T>(
        this IMongoCollection<T> mongoCollection,
        Expression<Func<T, bool>> where,
        Expression<Func<T>> update,
        UpdateOptions? options = null,
        CancellationToken cancellationToken = default
    ) =>
        await mongoCollection.UpdateManyAsync(
            where,
            new UpdateExpressionVisitor<T>().GetUpdateDefinition(update),
            options,
            cancellationToken
        );
}
