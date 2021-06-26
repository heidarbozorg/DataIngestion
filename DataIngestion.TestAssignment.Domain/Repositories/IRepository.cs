using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void AddRange(IEnumerable<TEntity> entities);
    }
}
