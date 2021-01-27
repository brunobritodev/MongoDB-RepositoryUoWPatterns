using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace MongoDB.GenericRepository.Persistence
{
    public static class MongoDbPersistence
    {
        [System.Obsolete]
        public static void Configure()
        {
            ProductMap.Configure();

            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

            // Conventions
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }
    }
}
