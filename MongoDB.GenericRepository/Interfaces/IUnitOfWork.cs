using System;

namespace MongoDB.GenericRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
