namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class CollectionMatch : CsvFiles<Domain.Entities.CollectionMatch>
    {
        public CollectionMatch(string fileAddress) : base(fileAddress)
        {
        }

        protected override Domain.Entities.CollectionMatch Parse(string[] splitedStr)
        {
            if (splitedStr == null || splitedStr.Length != 5)
                return null;

            var rst = new Domain.Entities.CollectionMatch()
            {
                CollectionId = long.Parse(splitedStr[1]),
                Upc = splitedStr[2]
            };

            return rst;
        }
    }
}
