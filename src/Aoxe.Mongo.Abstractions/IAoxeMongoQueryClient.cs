using System.Linq;

namespace Aoxe.Mongo.Abstractions;

public interface IAoxeMongoQueryClient
{
    IQueryable<T> GetQueryable<T>()
        where T : class;
}
