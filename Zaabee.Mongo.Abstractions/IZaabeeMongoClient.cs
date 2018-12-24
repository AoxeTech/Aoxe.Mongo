using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zaabee.Mongo.Abstractions
{
    public interface IZaabeeMongoClient : IZaabeeMongoQueryClient
    {
        void Add<T>(T entity) where T : class, new();
        Task AddAsync<T>(T entity) where T : class, new();
        void AddRange<T>(IEnumerable<T> entities) where T : class, new();
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class, new();
        long Delete<T>(T entity) where T : class, new();
        Task<long> DeleteAsync<T>(T entity) where T : class, new();
        long Delete<T>(Expression<Func<T, bool>> where) where T : class, new();
        Task<long> DeleteAsync<T>(Expression<Func<T, bool>> where) where T : class, new();
        long Update<T>(T entity) where T : class, new();
        Task<long> UpdateAsync<T>(T entity) where T : class, new();
        long Update<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class, new();
        Task<long> UpdateAsync<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class, new();
    }
}