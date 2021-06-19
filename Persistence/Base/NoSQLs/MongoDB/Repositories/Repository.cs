using System.Linq;
using System.Threading.Tasks;
using PersistenceLayer.Base.NoSQLs.MongoDB.Repositories.Interfaces;
using MongoDB.Driver;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using DomainLayer.Base.Interfaces;
using MongoDB.Bson.Serialization;
using PersistenceLayer.Base.NoSQLs.MongoDB.Extensions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace PersistenceLayer.Base.NoSQLs.MongoDB.Repositories
{
    public class Repository<T> : IReadWriteRepository<T> where T : IDomainEntityRepositorable
    {
        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<T> _collection;

        private Repository() { }

        protected internal Repository(IMongoContext context)
        {
            _context = context;
            var bsonClassMap = BsonClassMap.LookupClassMap(typeof(T));
            var collectionName = bsonClassMap.GetCollectionName();

            _collection = bsonClassMap.CreateIndex(_context.GetCollection<T>(collectionName));
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return await Task.Run(() => _collection.AsQueryable().Where(expression ?? (_ => true))).ConfigureAwait(false);
        }

        public async Task<T> GetAsync(string id)
        {
            var data = await _collection.FindAsync(Builders<T>.Filter.Eq("_id", id)).ConfigureAwait(false);

            return data.FirstOrDefault();
        }

        public async Task InsertAsync(T entity)
        {
            await Task
                .Run(() => _context.AddCommand(async () => await _collection.InsertOneAsync(entity).ConfigureAwait(false)))
                .ConfigureAwait(false);
        }

        public async Task InsertAsync(IEnumerable<T> entitys)
        {
            await Task
                .Run(() => _context.AddCommand(async () => await _collection.InsertManyAsync(entitys).ConfigureAwait(false)))
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task
                .Run(() => _context.AddCommand(async () => await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", entity.Id), entity).ConfigureAwait(false)))
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task
                .Run(() => _context.AddCommand(() => _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", entity.Id))))
                .ConfigureAwait(false);
        }
    }
}
