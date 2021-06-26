using Nest;
using System;

namespace DataIngestion.TestAssignment.Infrastructure
{
    public class UnitOfWork : Domain.Repositories.IUnitOfWork
    {
        private readonly ElasticClient _client;
        public Domain.Repositories.ICollectionRepository Collections { get; private set; }

        public UnitOfWork(string elasticSearchUrl)
        {
            var settings = new ConnectionSettings(new Uri(elasticSearchUrl))
                .DefaultIndex("collections");
            _client = new ElasticClient(settings);
            Collections = new NoSQL.CollectionRepository(_client);
        }

        public void Complete()
        {
            //Save transactions
        }

        public void Dispose()
        {
            //Close db connection and dispose client
        }
    }
}