using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;


namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public class GoogleDrive : Domain.FileIO.IGoogleDrive
    {
        private readonly string _googleDriveAccessKey;
        private readonly string _googleDriveBaseUrl;

        public GoogleDrive(string googleDriveAccessKey,
            string googleDriveBaseUrl = "https://www.googleapis.com/drive/v2/files?q=")
        {
            _googleDriveAccessKey = googleDriveAccessKey;
            _googleDriveBaseUrl = googleDriveBaseUrl;
        }

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
            var googleDriveApiURL = _googleDriveBaseUrl + folderId + "+in+parents&key=" + _googleDriveAccessKey;

            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;

            var client = new RestClient(googleDriveApiURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "aplication/json");
            client.Timeout = 30000;

            var response = client.Execute<GoogleDriveList>(request);
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

        public IEnumerable<string> Download(ICollection<Domain.FileIO.GoogleDriveItem> files)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.FileIO.GoogleDriveItem> GetFilesInfo(
            string googleDriveFolderAddress, 
            string extensionFilter = null)
        {
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