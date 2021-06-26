using System;
using System.Collections.Generic;
using System.Text;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class Artist : CsvFileRepository<Domain.Entities.Artist>
    {
        protected Artist(string fileAddress) : base(fileAddress)
        {
        }
    }
}
