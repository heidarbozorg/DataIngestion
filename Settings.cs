using Microsoft.Extensions.Configuration;

namespace DataIngestion.TestAssignment
{
    public class Settings
    {
        private readonly string _baseAddress;
        private readonly IConfigurationRoot _config;

        public string GoogleDriveBaseUrl { get; }
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
        

        public Settings(string baseAddress)
        {
            _baseAddress = baseAddress;

            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            _config = builder.Build();

            GoogleDriveBaseUrl = _config["GoogleDriveBaseUrl"];
            GoogleDriveAccessKey = _config["GoogleDriveAccessKey"];
            GoogleDriveFolderAddress = _config["GoogleDriveFolderAddress"];

            DownloadFolder = GetFolder("DownloadFolder") + "\\";
            UnzipFolder = GetFolder("UnzipFolder") + "\\";
            ElasticSearchUrl = _config["ElasticSearchUrl"];
        }
    }
}
