using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDB.GenericRepository.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        Task AddCommand(Func<Task> func);
        int SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}