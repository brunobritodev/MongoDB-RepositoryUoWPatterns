using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDB.GenericRepository.Model
{
    public class Product
    {
        public Product(string description)
        {
            Description = description;
            Id = Guid.NewGuid();
        }

        public Product(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        [BsonId]
        public Guid Id { get; private set; }

        public string Description { get; private set; }
    }
}