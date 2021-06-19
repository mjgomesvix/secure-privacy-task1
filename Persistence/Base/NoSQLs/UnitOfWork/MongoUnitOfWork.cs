using DomainLayer.Base.Interfaces;
using PersistenceLayer.Base.NoSQLs.MongoDB.Repositories;
using PersistenceLayer.Base.NoSQLs.MongoDB.Repositories.Interfaces;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using System.Threading.Tasks;

namespace PersistenceLayer.Base.NoSQLs.UnitOfWork
{
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        protected readonly IMongoContext _context;

        protected internal MongoUnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public IMongoContext Context => _context;

        public IReadWriteRepository<T> GetRepository<T>() where T : IDomainEntityRepositorable
        {
            return new Repository<T>(_context);
        }
        public async Task<bool> CommitAsync()
        {
            var changeAmount = await _context.CommitChanges().ConfigureAwait(false);

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
