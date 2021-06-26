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
            return null;
        }
    }
}
