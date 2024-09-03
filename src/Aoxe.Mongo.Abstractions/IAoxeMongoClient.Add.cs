namespace Aoxe.Mongo.Abstractions;

public partial interface IAoxeMongoClient : IAoxeMongoQueryClient
{
    void Add<T>(T entity)
        where T : class;
    ValueTask AddAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class;
    void AddRange<T>(IEnumerable<T> entities)
        where T : class;
    ValueTask AddRangeAsync<T>(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default
    )
        where T : class;
}
