using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.FileIO
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
