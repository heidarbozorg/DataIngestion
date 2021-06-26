namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class ArtistRepository : CsvFileRepository<Domain.Entities.Artist>
    {
        public ArtistRepository(string fileAddress) : base(fileAddress)
        {
        }

        protected override Domain.Entities.Artist Parse(string[] splitedStr)
        {
            if (splitedStr == null || splitedStr.Length != 6)
                return null;

            var rst = new Domain.Entities.Artist()
            {
                ArtistId = long.Parse(splitedStr[1]),
                Name = splitedStr[2]
            };

            return rst;
        }
    }
}