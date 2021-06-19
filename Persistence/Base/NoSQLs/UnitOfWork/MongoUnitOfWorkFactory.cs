using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using System;

namespace PersistenceLayer.Base.NoSQLs.UnitOfWork
{
    public class MongoUnitOfWorkFactory : IMongoUnitOfWorkFactory
    {
        public IMongoUnitOfWork Create() => new MongoUnitOfWork(ContextFactory);

        #region Members

        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        private readonly object _lock = new object();

        #endregion

        #region Construtores

        public MongoUnitOfWorkFactory(
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        #endregion        

        #region Internal / Protected Methods

        protected internal IMongoContext ContextFactory
        {
            get
            {
                lock (_lock)
                {
                    const string sessionFactoryIdentifier = "SessionFactory";
                    var contextFactory = _memoryCache.Get<IMongoContext>(sessionFactoryIdentifier);

                    if (contextFactory != null)
                        return contextFactory;

                    contextFactory = new MongoContext(_configuration);

                    // keep sessionFactory in cache as long as it is requested at least
                    // once every 5 minutes...
                    // but in any case make sure to refresh it every hou
                    /*_memoryCache.Set(
                        sessionFactoryIdentifier,
                        contextFactory,
                        new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                    */

                    if (contextFactory == null)
                        throw new InvalidOperationException("BuildSessionFactory is null.");

                    return contextFactory;
                }
            }
        }

        #endregion
    }
}
