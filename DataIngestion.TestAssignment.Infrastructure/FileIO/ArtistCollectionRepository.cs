namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class ArtistCollectionRepository : CsvFileRepository<Domain.Entities.ArtistCollection>
    {
        public ArtistCollectionRepository(string fileAddress) : base(fileAddress)
        {
        }

        protected override Domain.Entities.ArtistCollection Parse(string[] splitedStr)
        {
            if (splitedStr == null || splitedStr.Length != 6)
                return null;

            var rst = new Domain.Entities.ArtistCollection()
            {
                ArtistId = long.Parse(splitedStr[1]),
                CollectionId = long.Parse(splitedStr[2])
            };

            return rst;
        }
    }
}
