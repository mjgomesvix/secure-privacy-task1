using DomainLayer.Entities.OrderModel;
using MongoDB.Bson.Serialization;
using PersistenceLayer.Base.NoSQLs.MongoDB.Extensions;

namespace PersistenceLayer.NoSQLs.MongoDB.Mappings
{
    public static class OrderMongoDbMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Order>(map =>
            {
                map.SetCollectionName("orders");

                map.SetIgnoreExtraElements(true);

                map
                    .MapIdProperty(o => o.Id)
                    .SetElementName("id")
                    .SetIsRequired(true);

                map
                    .MapProperty(o => o.CreatedAt)
                    .SetElementName("createdAt")
                    .SetIsRequired(true)
                    .SetIsIndex();

                map
                    .MapMember(o => o.PersonId)
                    .SetIsRequired(true)
                    .SetElementName("personId")
                    .SetIsIndex();

                map.UnmapMember(o => o.Items);
            });
        }
    }
}
