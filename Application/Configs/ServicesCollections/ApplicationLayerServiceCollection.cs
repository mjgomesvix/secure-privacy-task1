using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using PersistenceLayer.Base.NoSQLs.UnitOfWork;
using PersistenceLayer.NoSQLs.MongoDB.Mappings;

namespace ApplicationLayer.Configs.ServicesCollections
{
    public static class ApplicationLayerServiceCollection
    {
        public static void ApplicationLayerServiceCollectionLoad(
            this IServiceCollection services)
        {
            services.AddSingleton<IMongoUnitOfWorkFactory, MongoUnitOfWorkFactory>();

            MongoDbMappings.Configure();
        }
    }
}
