using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Zaabee.Mongo.Abstractions
{
    public interface IMongoDbRepository
    {
        IQueryable<T> GetQueryable<T>() where T : class, new();
        void Add<T>(T entity) where T : class, new();
        void AddRange<T>(IEnumerable<T> entities) where T : class, new();
        long Delete<T>(T entity) where T : class, new();
        long Delete<T>(Expression<Func<T, bool>> where) where T : class, new();
        long Update<T>(T entity) where T : class, new();
    }
}