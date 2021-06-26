using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.Repositories
{
    public interface ICsvFiles<TEntity> where TEntity : class
    {
        /// <summary>
        /// Read Csv file and convert it to List
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();
    }
}
