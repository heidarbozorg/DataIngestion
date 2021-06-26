using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class GoogleDriveList
    {
        public IEnumerable<Domain.FileIO.GoogleDriveItem> Items { get; set; }

        public GoogleDriveList()
        {
            Items = new List<Domain.FileIO.GoogleDriveItem>();
        }
    }
}
