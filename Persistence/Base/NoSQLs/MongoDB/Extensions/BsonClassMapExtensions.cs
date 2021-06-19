using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace PersistenceLayer.Base.NoSQLs.MongoDB.Extensions
{
    public static class BsonClassMapExtensions
    {
        private static readonly ConcurrentDictionary<Type, string> _cache = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<Type, Index> _indexesToCreate = new ConcurrentDictionary<Type, Index>();

        public static string GetCollectionName(this BsonClassMap classMap)
        {
            if (_cache.TryGetValue(classMap.ClassType, out string result))
                return result;
            else
                return classMap.ClassType.Name;
        }

        public static IMongoCollection<T> CreateIndex<T>(this BsonClassMap classMap, IMongoCollection<T> collection)
        {
            var indices = _indexesToCreate.Where(i => i.Key == classMap.ClassType).Select(i =>
            {
                if (!_cache.TryGetValue(i.Key, out string collectionName))
                    collectionName = i.Key.Name;

                return new { collectionName, i.Value.Name, i.Value.IsUnique };
            });

            foreach (var index in indices)
            {
                var options = new CreateIndexOptions { Unique = index.IsUnique, Name = $"{index.collectionName}_{index.Name}" };
                var createIndexModel = new CreateIndexModel<T>($"{{ {index.Name} : 1 }}", options);

                collection.Indexes.CreateOne(createIndexModel);
            }

            return collection;
        }

        public static BsonClassMap SetCollectionName(this BsonClassMap classMap, string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new InvalidOperationException("Collection name must be valid string.");

            _cache[classMap.ClassType] = collectionName;

            return classMap;
        }

        public static BsonMemberMap SetIsIndex(this BsonMemberMap bsonMemberMap, bool isUnique = false)
        {
            _indexesToCreate[bsonMemberMap.ClassMap.ClassType] = new Index { Name = bsonMemberMap.ElementName, IsUnique = isUnique };

            return bsonMemberMap;
        }

        private class Index
        {
            public string Name { get; set; }
            public bool IsUnique { get; set; }
        }
    }
}
