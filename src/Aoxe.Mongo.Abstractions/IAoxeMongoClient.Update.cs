namespace Aoxe.Mongo.Abstractions;

public partial interface IAoxeMongoClient
{
    long Update<T>(T entity)
        where T : class;
    ValueTask<long> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class;
    long Update<T>(Expression<Func<T, bool>> where, Expression<Func<T>> update)
        where T : class;
    ValueTask<long> UpdateAsync<T>(
        Expression<Func<T, bool>> where,
        Expression<Func<T>> update,
        CancellationToken cancellationToken = default
    )
        where T : class;
}
