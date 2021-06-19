using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DomainLayer.Base.Interfaces
{
    public interface IRepository<T> where T : IDomainEntityRepositorable
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<T> GetAsync(string id);
    }
}
