using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.FileIO
{
    public interface IGoogleDrive
    {
        IEnumerable<GoogleDriveItem> GetListOfFiles(string googleDriveFolderAddress,
                        string extensionFilter = null);

        IEnumerable<string> Download(IEnumerable<GoogleDriveItem> files);
    }
}
