namespace DataIngestion.TestAssignment.Domain.Entities
{
    public class ArtistCollection
    {
        public long ExportDate { get; set; }
        public long ArtistId { get; set; }
        public long CollectionId { get; set; }
        public bool IsPrimaryArtist { get; set; }
        public int RoleId { get; set; }
    }
}
