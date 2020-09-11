using System;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}