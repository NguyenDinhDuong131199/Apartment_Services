using System;

namespace ApartmentManagement.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
