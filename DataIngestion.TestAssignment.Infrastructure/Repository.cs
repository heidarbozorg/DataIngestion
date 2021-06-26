using Nest;
using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Infrastructure
{
    public class Repository<TEntity> : 
        Domain.Repositories.IRepository<TEntity> where TEntity : class
    {
        private const int _pageSize = 512;

        private readonly ElasticClient _client;        

        public Repository(ElasticClient client)
        {
           _client = client;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _client.BulkAll(entities, e => e
                .BackOffTime("30s")
                .BackOffRetries(2)
                .RefreshOnCompleted()
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                .Size(_pageSize)
            )
            .Wait(TimeSpan.FromMinutes(15), next =>
            {
            });
        }
    }
}