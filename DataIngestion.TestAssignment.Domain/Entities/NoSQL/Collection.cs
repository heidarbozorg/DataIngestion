using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.Entities.NoSQL
{
    public class Collection
    {
        public long id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string upc { get; set; }
        public DateTime releaseDate { get; set; }
        public bool isCompilation { get; set; }
        public string label { get; set; }
        public string imageUrl { get; set; }
        public ICollection<Artist> artists { get; set; }

        public Collection(
            Entities.Collection collection,
            Entities.CollectionMatch collectionMatch,
            List<Artist> artists
            )
        {
            id = collection.CollectionId;
            name = collection.Name;
            url = collection.ViewUrl;
            releaseDate = collection.OriginalReleaseDate;
            isCompilation = collection.IsCompilation;
            label = collection.LabelStudio;
            imageUrl = collection.ArtworkUrl;

            upc = collectionMatch.Upc;
            this.artists = artists;
        }
    }
}