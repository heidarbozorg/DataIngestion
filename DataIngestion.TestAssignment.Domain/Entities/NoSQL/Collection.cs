using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.Entities.NoSQL
{
    public class Collection
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Upc { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsCompilation { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Artist> Artists { get; set; }

        public Collection(
            Entities.Collection collection,
            Entities.CollectionMatch collectionMatch,
            List<Artist> artists
            )
        {
            Id = collection.CollectionId;
            Name = collection.Name;
            Url = collection.ViewUrl;
            ReleaseDate = collection.OriginalReleaseDate;
            IsCompilation = collection.IsCompilation;
            Label = collection.LabelStudio;
            ImageUrl = collection.ArtworkUrl;

            Upc = collectionMatch.Upc;
            Artists = artists;
        }
    }
}