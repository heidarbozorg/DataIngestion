using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;


namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class GoogleDrive : Domain.FileIO.IGoogleDrive
    {
        #region private fields
        private readonly string _googleDriveAccessKey;
        private readonly string _googleDriveAPIBaseUrl;
        private readonly Domain.FileIO.IDownloader _downloader;
        private readonly string _downloadFolder;
        private IRestClient _restClient;
        #endregion

        #region private methods
        private string GetFolderId(string googleDriveFolderAddress)
        {
            if (string.IsNullOrEmpty(googleDriveFolderAddress))
                return null;

            var split = googleDriveFolderAddress.Split("/");
            var rst = split[split.Length - 1];

            return "'" + rst + "'";
        }

        private IRestResponse<GoogleDriveList> CallGoogleDriveAPI(string googleDriveFolderAddress)
        {
            var folderId = GetFolderId(googleDriveFolderAddress);
            var googleDriveApiURL = _googleDriveAPIBaseUrl + folderId + "+in+parents&key=" + _googleDriveAccessKey;

            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;

            if (_restClient == null)
            {
                _restClient = new RestClient(googleDriveApiURL);
                _restClient.Timeout = 30000;
            }

            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "aplication/json");

            var response = _restClient.Execute<GoogleDriveList>(request);
            return response;
        }

        private IEnumerable<Domain.FileIO.GoogleDriveItem> ParseRestResponse(IRestResponse<GoogleDriveList> restResponse)
        {
            if (restResponse != null && restResponse.Data != null && restResponse.IsSuccessful)
            {
                var driveList = restResponse.Data.Items;
                return driveList;
            }

            return null;
        }
        #endregion

        public GoogleDrive(
                        string googleDriveAccessKey,                        
                        Domain.FileIO.IDownloader downloader,
                        string downloadFolder,                        
                        string googleDriveAPIBaseUrl = "https://www.googleapis.com/drive/v2/files?q=",
                        IRestClient restClient = null)
        {
            _googleDriveAccessKey = googleDriveAccessKey;
            _googleDriveAPIBaseUrl = googleDriveAPIBaseUrl;
            _downloader = downloader;
            _downloadFolder = downloadFolder;
            _restClient = restClient;
        }

        public IEnumerable<string> Download(IEnumerable<Domain.FileIO.GoogleDriveItem> files)
        {
            if (files == null || files.Count() == 0)
                return null;

            var rst = new List<string>();
            foreach (var f in files)
            {
                var destionationFileAddress = _downloadFolder + f.Title;
                _downloader.Download(f.DownloadUrl ?? f.AlternateLink,
                                        destionationFileAddress);

                rst.Add(destionationFileAddress);
            }

            return rst;
        }

        public IEnumerable<Domain.FileIO.GoogleDriveItem> GetListOfFiles(
            string googleDriveFolderAddress, 
            string extensionFilter = null)
        {
            if (string.IsNullOrWhiteSpace(googleDriveFolderAddress))
                throw new ArgumentNullException("Invalid Google Drive folder address");

            var restResponse = CallGoogleDriveAPI(googleDriveFolderAddress);
            var files = ParseRestResponse(restResponse);
            if (files == null)
                return null;

            if (string.IsNullOrEmpty(extensionFilter))
                return files;

            var filteredFiles = files.
                        Where(f => f.Title.ToLower().EndsWith(extensionFilter.ToLower()))
                        .ToList();
            return filteredFiles;
        }
    }
}