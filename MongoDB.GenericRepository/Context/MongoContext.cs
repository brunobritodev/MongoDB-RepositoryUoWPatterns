using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        private readonly List<Func<Task>> _commands;
        public MongoContext(IConfiguration configuration)
        {
            // Configure the persistence in another layer
            MongoDbPersistence.Configure();

            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();

            // Configure mongo (You can inject the config, just to simplify)
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGOCONNECTION") ?? configuration.GetSection("MongoSettings").GetSection("Connection").Value);
            
            Database = mongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASENAME") ?? configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
            
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public async Task<int> SaveChanges()
        {
            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
