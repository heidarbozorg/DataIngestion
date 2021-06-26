using System.Collections.Generic;

namespace DataIngestion.TestAssignment.Domain.FileIO
{
    public interface IGoogleDrive
    {
        IEnumerable<GoogleDriveItem> GetFilesInfo(string googleDriveFolderAddress,
                        string extensionFilter = null);

        IEnumerable<string> Download(ICollection<GoogleDriveItem> files);
    }
}
