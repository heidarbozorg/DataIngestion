using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIngestion.TestAssignment.Infrastructure.NoSQL
{
    public class CollectionRepository :
        Repository<Domain.Entities.NoSQL.Collection>,
        Domain.Repositories.ICollectionRepository
    {
        public CollectionRepository(ElasticClient client) : base(client)
        {
        }

        public IEnumerable<Domain.Entities.NoSQL.Collection> GetCollection
        (
            IEnumerable<Domain.Entities.Artist> artists, 
            IEnumerable<Domain.Entities.ArtistCollection> artistCollections, 
            IEnumerable<Domain.Entities.Collection> collections, 
            IEnumerable<Domain.Entities.CollectionMatch> collectionMatches
        )
        {
            var artistJoinCollection =
                        (from ac in artistCollections
                         join a in artists on ac.ArtistId equals a.ArtistId into g
                         select new
                         {
                             ac.CollectionId,
                             artists = g.Select(a => new Domain.Entities.NoSQL.Artist(a.ArtistId, a.Name)).ToList()
                         }).ToList();

            var q = (from c in collections
                     join ac in artistJoinCollection on c.CollectionId equals ac.CollectionId
                     join cm in collectionMatches on ac.CollectionId equals cm.CollectionId
                     select new Domain.Entities.NoSQL.Collection(c, cm, ac.artists)
                     ).ToList();

            return q;
        }
	}
}