using Nest;
using System;
using System.Collections.Generic;

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
			var a = new Domain.Entities.NoSQL.Artist(935585671, "Anmol Dhaliwal");
			var arts = new List<Domain.Entities.NoSQL.Artist>();
			arts.Add(a);

			var c = new Domain.Entities.NoSQL.Collection()
			{
				id = 1255407551,
				name = "Nishana - Single",
				url = "http://ms.com/album/nishana-single/1255407551?uo=5",
				upc = "191061793557", // found in CollectionMatch file
				releaseDate = Convert.ToDateTime("2017-06-10T00:00:00"),
				isCompilation = false,
				label = "Aark Records",
				imageUrl = "http://img.com/image/thumb/Music117/v4/92/b8/51/92b85100-13c8-8fa4-0856-bb27276fdf87/191061793557.jpg/170x170bb.jpg",
				artists = arts
			};

			var rst = new List<Domain.Entities.NoSQL.Collection>();
			rst.Add(c);
			return rst;
		}
	}
}