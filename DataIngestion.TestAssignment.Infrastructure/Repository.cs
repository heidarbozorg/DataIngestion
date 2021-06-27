using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIngestion.TestAssignment.Infrastructure
{
    public class Repository<TEntity> : 
        Domain.Repositories.IRepository<TEntity> where TEntity : class
    {
        private const int _pageSize = 512;
        private const int _timeout = 15;

        private readonly IElasticClient _client;        

        public Repository(IElasticClient client)
        {
           _client = client;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("Invalid entities.");

            long n = entities.Count();
            long sentRecords = 0;

            _client.BulkAll(entities, e => e
                .BackOffTime("30s")
                .BackOffRetries(2)
                .RefreshOnCompleted()
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                .Size(_pageSize)
            )
            .Wait(TimeSpan.FromMinutes(_timeout), next =>
            {
                sentRecords += next.Items.Count;
                Console.Write("\r --> Insert into ElasticSearch: {0}/{1} Records -- %{2}",
                        sentRecords, n, (sentRecords * 100 / n));
            });
        }
    }
}