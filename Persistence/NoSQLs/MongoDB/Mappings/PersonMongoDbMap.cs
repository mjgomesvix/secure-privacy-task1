using DomainLayer.Entities.PersonModel;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PersistenceLayer.Base.NoSQLs.MongoDB.Extensions;
using System;

namespace PersistenceLayer.NoSQLs.MongoDB.Mappings
{
    public static class PersonMongoDbMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Person>(map =>
            {
                map.SetCollectionName("people");

                map.SetIgnoreExtraElements(true);

                map.MapIdProperty(p => p.Id)
                    .SetElementName("id")
                    .SetIsRequired(true);

                map.MapProperty(p => p.CreatedAt)
                    .SetElementName("createdAt")
                    .SetSerializer(new DateTimeSerializer(dateOnly: false))
                    .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc))
                    .SetIsRequired(true);

                map
                    .MapMember(x => x.Name)
                    .SetIsRequired(true)
                    .SetElementName("name")
                    .SetIsIndex();

                map
                    .MapMember(x => x.Birthday)
                    .SetIsRequired(true)
                    .SetSerializer(new DateTimeSerializer(dateOnly: true))
                    .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc))
                    .SetElementName("birthday");

                map.MapField("_phones").SetIsRequired(true).SetElementName("phones");
            });
        }
    }
}
