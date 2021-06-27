using System;
using Microsoft.Extensions.Configuration;

namespace DataIngestion.TestAssignment
{
    public class Settings
    {
        private readonly string _baseAddress;
        private readonly IConfigurationRoot _config;

        public string GoogleDriveAPIBaseUrl { get; }
        public string GoogleDriveAccessKey { get; }
        public string GoogleDriveFolderAddress { get; }

        public string DownloadFolder { get; }
        public string UnzipFolder { get; }
        public string ElasticSearchUrl { get; }

        private string GetFolder(string name)
        {
            var folderName = _config[name];
            var rst = _baseAddress + folderName;
            if (!System.IO.Directory.Exists(rst))
                System.IO.Directory.CreateDirectory(rst);

            return rst;
        }
        

        public Settings()
        {
            _baseAddress = Environment.CurrentDirectory + "\\";

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true);

            _config = builder.Build();

            GoogleDriveAPIBaseUrl = _config["GoogleDriveAPIBaseUrl"];
            GoogleDriveAccessKey = _config["GoogleDriveAccessKey"];
            if (string.IsNullOrEmpty(GoogleDriveAccessKey))
                throw new ArgumentNullException("Plaease set ASPNETCORE_ENVIRONMENT value, then set GoogleDriveAccessKey tag in the appsettings.ASPNETCORE_ENVIRONMENT.json");

            GoogleDriveFolderAddress = _config["GoogleDriveFolderAddress"];

            DownloadFolder = GetFolder("DownloadFolder") + "\\";
            UnzipFolder = GetFolder("UnzipFolder") + "\\";
            ElasticSearchUrl = _config["ElasticSearchUrl"];
        }
    }
}
