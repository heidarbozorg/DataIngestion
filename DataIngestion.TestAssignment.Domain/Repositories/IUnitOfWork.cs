using System;

namespace DataIngestion.TestAssignment.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICollectionRepository Collections { get; }

        void Complete();
    }
}
