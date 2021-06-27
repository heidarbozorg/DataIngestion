namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class ArtistCollection : CsvFiles<Domain.Entities.ArtistCollection>
    {
        public ArtistCollection(string fileAddress) : base(fileAddress)
        {
        }

        protected override Domain.Entities.ArtistCollection Parse(string[] splitedStr)
        {
            if (splitedStr == null || splitedStr.Length != 5)
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
