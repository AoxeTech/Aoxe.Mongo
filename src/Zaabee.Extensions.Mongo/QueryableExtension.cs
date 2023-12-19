namespace Zaabee.Extensions.Mongo;

public static class QueryableExtension
{
    public static async ValueTask<bool> AnyAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.AnyAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async ValueTask<T> FirstAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.FirstAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async ValueTask<T> FirstOrDefaultAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.FirstOrDefaultAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async ValueTask<T> SingleAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.SingleAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async ValueTask<T> SingleOrDefaultAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.SingleOrDefaultAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }

    public static async ValueTask<List<T>> ToListAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default
    )
    {
        if (queryable is IAsyncCursorSource<T> mongoQueryable)
            return await mongoQueryable.ToListAsync(cancellationToken);
        throw new ArgumentException($"{nameof(queryable)} has not implemented IAsyncCursorSource.");
    }
}
