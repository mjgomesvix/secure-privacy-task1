using DomainLayer.Base.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersistenceLayer.Base.NoSQLs.MongoDB.Repositories.Interfaces
{
    public interface IReadWriteRepository<T> : IRepository<T> where T : IDomainEntityRepositorable
    {
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entitys);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
