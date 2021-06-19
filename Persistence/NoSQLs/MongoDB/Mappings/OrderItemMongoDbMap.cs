using DomainLayer.Entities.OrderItemModel;
using MongoDB.Bson.Serialization;
using PersistenceLayer.Base.NoSQLs.MongoDB.Extensions;

namespace PersistenceLayer.NoSQLs.MongoDB.Mappings
{
    public static class OrderItemMongoDbMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<OrderItem>(map =>
            {
                map.SetCollectionName("orderItems");

                map.SetIgnoreExtraElements(true);

                map.MapIdProperty(op => op.Id)
                    .SetElementName("id")
                    .SetIsRequired(true);

                map.MapProperty(op => op.CreatedAt)
                    .SetElementName("createdAt")
                    .SetIsRequired(true);

                map.MapMember(op => op.OrderId).SetIsRequired(true).SetElementName("orderId");
                map.MapMember(op => op.ProductId).SetIsRequired(true).SetElementName("productId");
                map.MapMember(op => op.Quantity).SetIsRequired(true).SetElementName("quantity");
                map.MapMember(op => op.UnityPrice).SetIsRequired(true).SetElementName("unityPrice");
                map.UnmapMember(op => op.TotalPrice);
            });
        }
    }
}
