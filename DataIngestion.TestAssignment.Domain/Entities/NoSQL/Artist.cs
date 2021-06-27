namespace DataIngestion.TestAssignment.Domain.Entities.NoSQL
{
    public class Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public Artist(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}