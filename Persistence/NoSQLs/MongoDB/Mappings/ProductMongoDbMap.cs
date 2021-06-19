using DomainLayer.Entities.ProductModel;
using MongoDB.Bson.Serialization;
using PersistenceLayer.Base.NoSQLs.MongoDB.Extensions;

namespace PersistenceLayer.NoSQLs.MongoDB.Mappings
{
    public static class ProductMongoDbMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.SetCollectionName("products");

                map.SetIgnoreExtraElements(true);

                map.MapIdProperty(p => p.Id)
                    .SetElementName("id")
                    .SetIsRequired(true);

                map.MapProperty(p => p.CreatedAt)
                    .SetElementName("createdAt")
                    .SetIsRequired(true);

                map.MapMember(x => x.Name).SetIsRequired(true).SetElementName("name");
                map.MapMember(x => x.Unity).SetIsRequired(true).SetElementName("unity");
                map.MapMember(x => x.Price).SetIsRequired(true).SetElementName("price");
            });
        }
    }
}
