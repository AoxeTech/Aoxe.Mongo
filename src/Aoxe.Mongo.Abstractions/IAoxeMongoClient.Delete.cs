namespace Aoxe.Mongo.Abstractions;

public partial interface IAoxeMongoClient
{
    long Delete<T>(T entity)
        where T : class;
    ValueTask<long> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class;
    long DeleteMany<T>(Expression<Func<T, bool>> where)
        where T : class;
    ValueTask<long> DeleteAsync<T>(
        Expression<Func<T, bool>> where,
        CancellationToken cancellationToken = default
    )
        where T : class;
}
