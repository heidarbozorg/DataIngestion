using System;
using System.Net;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class Downloader : Domain.FileIO.IDownloader
    {
        private readonly WebClient _webClient;
        private string _fileName;

        public Downloader()
        {
            _webClient = new WebClient();
            _webClient.DownloadProgressChanged += _webClient_DownloadProgressChanged;
            _webClient.DownloadFileCompleted += _webClient_DownloadFileCompleted;
        }

        private void _webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("\r --> File {0} has been successfully downloaded.",
                _fileName);
        }

        private void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\r --> Downloading {0} : {1} KB -- %{2}",
                _fileName,
                string.Format("{0:n0}", e.BytesReceived / 1000),
                e.ProgressPercentage);
        }

        public void Download(string url, string destinationFileAddress)
        {
            _fileName = System.IO.Path.GetFileName(destinationFileAddress);

            _webClient.DownloadFileAsync(new Uri(url), destinationFileAddress);

            while (_webClient.IsBusy)
                System.Threading.Thread.Sleep(1000);
        }


        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}