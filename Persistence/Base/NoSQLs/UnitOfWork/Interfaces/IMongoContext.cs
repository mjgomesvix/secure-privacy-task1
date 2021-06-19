using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> CommitChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}