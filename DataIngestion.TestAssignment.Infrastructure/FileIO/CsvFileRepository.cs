using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class CsvFileRepository<TEntity> : Domain.Repositories.ICsvFiles<TEntity> where TEntity : class
    {
        private readonly string _fileAddress;

        protected CsvFileRepository(string fileAddress)
        {
            _fileAddress = fileAddress;
        }

        public List<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
