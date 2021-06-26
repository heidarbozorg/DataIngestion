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
    }
}