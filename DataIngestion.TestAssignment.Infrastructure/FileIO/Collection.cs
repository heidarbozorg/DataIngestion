using System;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class Collection : CsvFiles<Domain.Entities.Collection>
    {
        public Collection(string fileAddress) : base(fileAddress)
        {
        }

        private bool GetBool(string str)
        {
            int temp;
            int.TryParse(str, out temp);
            return temp == 1;
        }

        protected override Domain.Entities.Collection Parse(string[] splitedStr)
        {
            if (splitedStr == null || splitedStr.Length != 18)
                return null;

            var rst = new Domain.Entities.Collection()
            {
                CollectionId = long.Parse(splitedStr[1]),

                Name = splitedStr[2],
                ViewUrl = splitedStr[7],
                ArtworkUrl = splitedStr[8],
                OriginalReleaseDate = Convert.ToDateTime(splitedStr[9]),
                LabelStudio = splitedStr[11],
                IsCompilation = GetBool(splitedStr[16]),
            };

            return rst;
        }
    }
}
