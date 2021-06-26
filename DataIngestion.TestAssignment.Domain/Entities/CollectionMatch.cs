namespace DataIngestion.TestAssignment.Domain.Entities
{
    public class CollectionMatch
    {
        public long ExportDate { get; set; }
        public long CollectionId { get; set; }
        public string Upc { get; set; }
        public string Grid { get; set; }
        public int AmgAlbumId { get; set; }
    }
}
