using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zaabee.Mongo.Abstractions
{
    public interface IZaabeeMongoClient : IZaabeeMongoQueryClient
    {
        void Add<T>(T entity) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entities) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        long Delete<T>(T entity) where T : class;
        Task<long> DeleteAsync<T>(T entity) where T : class;
        long Delete<T>(Expression<Func<T, bool>> where) where T : class;
        Task<long> DeleteAsync<T>(Expression<Func<T, bool>> where) where T : class;
        long Update<T>(T entity) where T : class;
        Task<long> UpdateAsync<T>(T entity) where T : class;
        long Update<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class;
        Task<long> UpdateAsync<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class;
    }
}