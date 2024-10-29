using MongoDB.Driver;
using MongoDBLib.Model.Page;
using System.Linq.Expressions;

namespace MongoDBLib.Service
{
    /// <summary>
    /// MongoDb操作服务接口
    /// </summary>
    public interface IMongoService
    {
        void Add<T>(string collection, T entity);

        void Add<T>(T entity) where T : class;

        Task AddAsync<T>(string collection, T entity);

        Task AddAsync<T>(T entity) where T : class;

        Task BatchAddAsync<T>(string collection, List<T> entity);

        Task BatchAddAsync<T>(List<T> entity) where T : class;

        void BatchAdd<T>(string collection, List<T> entity);

        void BatchAdd<T>(List<T> entity) where T : class;

        long Delete<T>(Expression<Func<T, bool>> predicate) where T : class;

        long Delete<T>(string collection, Expression<Func<T, bool>> predicate);

        long DeleteOne(string collection, string id);

        Task<long> DeleteOneAsync(string collection, string id);

        Task<long> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<long> DeleteAsync<T>(string collection, Expression<Func<T, bool>> predicate);

        long Update<T>(Expression<Func<T, bool>> predicate, T entity) where T : class;

        long Update<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda) where T : class;

        long Update<T>(string collection, Expression<Func<T, bool>> predicate, T entity);

        long Update<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda);

        Task<long> UpdateAsync<T>(Expression<Func<T, bool>> predicate, T entity) where T : class;

        Task<long> UpdateAsync<T>(string collection, Expression<Func<T, bool>> predicate, T entity);

        Task<long> UpdateAsync<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda);

        Task<long> UpdateAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda) where T : class;

        T Get<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class;

        T Get<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class;

        Task<T> GetAsync<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector);

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector = null, int? limit = null, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class;

        Task<List<T>> GetListAsync<T>(string collection, Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int? limit, Expression<Func<T, object>> orderBy = null, bool desc = false);

        Task<MongoPageList<T>> GetPageListAsync<T>(string collection, Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class;

        Task<MongoPageList<T>> GetPageListAsync<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class;

        void Transaction(Action<IMongoDatabase, IClientSessionHandle> action);
    }
}
