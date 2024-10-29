using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBLib.Attributes;
using MongoDBLib.Extension;
using MongoDBLib.Model.Config;
using MongoDBLib.Model.Page;
using System.Linq.Expressions;

namespace MongoDBLib.Service
{
    #region MongoDb操作封装
    /// <summary>
    /// MongoDb操作服务
    /// </summary>
    public class MongoService : IMongoService
    {
        private MongoClient _client;

        private IMongoDatabase _db;

        private string _dbName;

        public MongoService(IOptions<MongoConfig> mongoConfig)
        {
            try
            {
                _client = new MongoClient(mongoConfig.Value.ConnectionString ?? "");
                _dbName = mongoConfig.Value.Database;
                _db = GetDatabase(_dbName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetCollection<T>() where T : class
        {
            MongoAttribute mongoAttribute = typeof(T).GetMongoAttribute();
            if (mongoAttribute != null)
            {
                return mongoAttribute.Collection;
            }

            return typeof(T).Name;
        }

        private IMongoDatabase GetDatabase(string database)
        {
            return _client.GetDatabase(database);
        }

        public void SetDatabase(string database)
        {
            _dbName = database;
            _db = GetDatabase(database);
        }

        public void Add<T>(string collection, T entity)
        {
            _db.GetCollection<T>(collection).InsertOne(entity);
        }

        public void Add<T>(T entity) where T : class
        {
            Add(GetCollection<T>(), entity);
        }

        public async Task AddAsync<T>(string collection, T entity)
        {
            await _db.GetCollection<T>(collection).InsertOneAsync(entity).ConfigureAwait(continueOnCapturedContext: false);
        }

        public Task AddAsync<T>(T entity) where T : class
        {
            return AddAsync(GetCollection<T>(), entity);
        }

        public async Task BatchAddAsync<T>(string collection, List<T> entity)
        {
            await _db.GetCollection<T>(collection).InsertManyAsync(entity).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task BatchAddAsync<T>(List<T> entity) where T : class
        {
            await _db.GetCollection<T>(GetCollection<T>()).InsertManyAsync(entity).ConfigureAwait(continueOnCapturedContext: false);
        }

        public void BatchAdd<T>(string collection, List<T> entity)
        {
            _db.GetCollection<T>(collection).InsertMany(entity);
        }

        public void BatchAdd<T>(List<T> entity) where T : class
        {
            _db.GetCollection<T>(GetCollection<T>()).InsertMany(entity);
        }

        public long Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DeleteAsync(predicate).Result;
        }

        public long Delete<T>(string collection, Expression<Func<T, bool>> predicate)
        {
            return DeleteAsync(collection, predicate).Result;
        }

        public long DeleteOne(string collection, string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            return _db.GetCollection<BsonDocument>(collection).DeleteOne(filter).DeletedCount;
        }

        public async Task<long> DeleteOneAsync(string collection, string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            return (await _db.GetCollection<BsonDocument>(collection).DeleteOneAsync(filter)).DeletedCount;
        }

        public Task<long> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DeleteAsync(GetCollection<T>(), predicate);
        }

        public async Task<long> DeleteAsync<T>(string collection, Expression<Func<T, bool>> predicate)
        {
            return (await _db.GetCollection<T>(collection).DeleteManyAsync(predicate).ConfigureAwait(continueOnCapturedContext: false)).DeletedCount;
        }

        public long Update<T>(Expression<Func<T, bool>> predicate, T entity) where T : class
        {
            return UpdateAsync(predicate, entity).Result;
        }

        public long Update<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda) where T : class
        {
            return UpdateAsync(predicate, lambda).Result;
        }

        public long Update<T>(string collection, Expression<Func<T, bool>> predicate, T entity)
        {
            return UpdateAsync(collection, predicate, entity).Result;
        }

        public long Update<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda)
        {
            return UpdateAsync(collection, predicate, lambda).Result;
        }

        public Task<long> UpdateAsync<T>(Expression<Func<T, bool>> predicate, T entity) where T : class
        {
            return UpdateAsync(GetCollection<T>(), predicate, entity);
        }

        public async Task<long> UpdateAsync<T>(string collection, Expression<Func<T, bool>> predicate, T entity)
        {
            IMongoCollection<T> collection2 = _db.GetCollection<T>(collection);
            UpdateDefinition<T> updateDefinition = entity.GetUpdateDefinition();
            UpdateDefinition<T> update = new UpdateDefinitionBuilder<T>().Combine(updateDefinition);
            return (await IMongoCollectionExtensions.UpdateOneAsync(collection2, predicate, update).ConfigureAwait(continueOnCapturedContext: false)).ModifiedCount;
        }

        public async Task<long> UpdateAsync<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda)
        {
            IMongoCollection<T> collection2 = _db.GetCollection<T>(collection);
            List<UpdateDefinition<T>> updateDefinition = MongoExpression<T>.GetUpdateDefinition(lambda);
            UpdateDefinition<T> update = new UpdateDefinitionBuilder<T>().Combine(updateDefinition);
            return (await IMongoCollectionExtensions.UpdateManyAsync(collection2, predicate, update).ConfigureAwait(continueOnCapturedContext: false)).ModifiedCount;
        }

        public Task<long> UpdateAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> lambda) where T : class
        {
            return UpdateAsync(GetCollection<T>(), predicate, lambda);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class
        {
            return Get(GetCollection<T>(), predicate, projector);
        }

        public T Get<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class
        {
            IFindFluent<T, T> find = _db.GetCollection<T>(collection).Find(predicate);
            if (projector != null)
            {
                find = find.Project(projector);
            }

            return find.FirstOrDefault();
        }

        public Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector = null) where T : class
        {
            return GetAsync(GetCollection<T>(), predicate, projector);
        }

        public async Task<T> GetAsync<T>(string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, T>> projector)
        {
            IFindFluent<T, T> find = _db.GetCollection<T>(collection).Find(predicate);
            if (projector != null)
            {
                find = find.Project(projector);
            }

            return await find.FirstOrDefaultAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector = null, int? limit = null, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class
        {
            return await GetListAsync(GetCollection<T>(), filter, projector, limit, orderBy, desc);
        }

        public async Task<List<T>> GetListAsync<T>(string collection, Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int? limit, Expression<Func<T, object>> orderBy = null, bool desc = false)
        {
            IFindFluent<T, T> findFluent = _db.GetCollection<T>(collection).Find(filter);
            if (projector != null)
            {
                findFluent = findFluent.Project(projector);
            }

            if (limit.HasValue)
            {
                findFluent = findFluent.Limit(limit);
            }

            if (orderBy != null)
            {
                findFluent = (desc ? findFluent.SortByDescending(orderBy) : findFluent.SortBy(orderBy));
            }

            return await findFluent.ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<MongoPageList<T>> GetPageListAsync<T>(string collection, Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class
        {
            IMongoCollection<T> coll = _db.GetCollection<T>(collection);
            long count = await IMongoCollectionExtensions.CountDocumentsAsync(coll, filter).ConfigureAwait(continueOnCapturedContext: false);
            IFindFluent<T, T> findFluent = coll.Find(filter);
            if (projector != null)
            {
                findFluent = findFluent.Project(projector);
            }

            if (orderBy != null)
            {
                findFluent = (desc ? findFluent.SortByDescending(orderBy) : findFluent.SortBy(orderBy));
            }

            return await findFluent.GetPageData(count, pageIndex, pageSize);
        }

        public async Task<MongoPageList<T>> GetPageListAsync<T>(Expression<Func<T, bool>> filter, Expression<Func<T, T>> projector, int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>> orderBy = null, bool desc = false) where T : class
        {
            string collection = GetCollection<T>();
            return await GetPageListAsync(collection, filter, projector, pageIndex, pageSize, orderBy, desc);
        }

        public void Transaction(Action<IMongoDatabase, IClientSessionHandle> action)
        {
            IClientSessionHandle clientSessionHandle = _client.StartSession();
            try
            {
                IMongoDatabase database = clientSessionHandle.Client.GetDatabase(_dbName);
                clientSessionHandle.StartTransaction();
                action(database, clientSessionHandle);
                clientSessionHandle.CommitTransaction();
            }
            catch (Exception ex)
            {
                clientSessionHandle.AbortTransaction();
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion
}
