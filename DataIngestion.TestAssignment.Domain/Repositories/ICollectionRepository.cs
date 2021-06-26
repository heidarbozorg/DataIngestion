using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.Repositories
{
    public interface ICollectionRepository : IRepository<Entities.NoSQL.Collection>
    {
        IEnumerable<Entities.NoSQL.Collection> GetCollection
        (
            IEnumerable<Entities.Artist> artists,
            IEnumerable<Entities.ArtistCollection> artistCollections,
            IEnumerable<Entities.Collection> collections,
            IEnumerable<Entities.CollectionMatch> collectionMatches
        );
    }
}
