namespace DataIngestion.TestAssignment.Domain.Entities.NoSQL
{
    public class Artist
    {
        public long id { get; set; }
        public string name { get; set; }

        public Artist(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}