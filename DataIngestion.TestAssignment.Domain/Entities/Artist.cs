namespace DataIngestion.TestAssignment.Domain.Entities
{
    public class Artist
    {
        public long ExportDate { get; set; }
        public long ArtistId { get; set; }
        public int ArtistTypeId { get; set; }
        public bool IsActualArtist { get; set; }
        public string Name { get; set; }
        public string ViewUrl { get; set; }
    }
}