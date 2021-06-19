namespace PersistenceLayer.NoSQLs.MongoDB.Mappings
{
    public static class MongoDbMappings
    {
        public static void Configure()
        {
            PersonMongoDbMap.Configure();
            ProductMongoDbMap.Configure();
            OrderMongoDbMap.Configure();
            OrderItemMongoDbMap.Configure();
        }
    }
}
