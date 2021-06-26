using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class Artist : CsvFileRepository<Domain.Entities.Artist>
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
                ExportDate = long.Parse(splitedStr[0]),
                ArtistId = long.Parse(splitedStr[1]),
                Name = splitedStr[2],
                IsActualArtist = int.Parse(splitedStr[3]) == 1,
                ViewUrl = splitedStr[4],
                ArtistTypeId = int.Parse(splitedStr[5])
            };

            return rst;
        }
    }
}
