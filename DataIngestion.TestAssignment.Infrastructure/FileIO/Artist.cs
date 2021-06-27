namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class Artist : CsvFiles<Domain.Entities.Artist>
    {
        public Artist(string fileAddress) : base(fileAddress)
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