using DomainLayer.Base.Interfaces;
using PersistenceLayer.Base.NoSQLs.MongoDB.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces
{
    public interface IMongoUnitOfWork : IDisposable
    {
        IMongoContext Context { get; }
        IReadWriteRepository<T> GetRepository<T>() where T : IDomainEntityRepositorable;
        Task<bool> CommitAsync();
    }
}