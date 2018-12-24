using System.Linq;

namespace Zaabee.Mongo.Abstractions
{
    public interface IZaabeeMongoQueryClient
    {
        IQueryable<T> GetQueryable<T>() where T : class, new();
    }
}