namespace Zaabee.Extensions.Mongo;

public static class QueryableExtension
{
    public static async Task<bool> AnyAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.AnyAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async Task<T> FirstAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.FirstAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.FirstOrDefaultAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async Task<T> SingleAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.SingleAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.SingleOrDefaultAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.ToListAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }
}