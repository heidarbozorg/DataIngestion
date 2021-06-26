using System;

namespace DataIngestion.TestAssignment.Domain.FileIO
{
    public interface IDownloader : IDisposable
    {
        void Download(string url, string destinationFileAddress);
    }
}
