using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace DataIngestion.TestAssignment.Infrastructure.UnitTests.FileIO
{
    [TestFixture]
    class GoogleDriveTests
    {
        private const string _accessKey = "Google Drive API key";
        private Infrastructure.FileIO.GoogleDrive _googleDrive;
        private Mock<Domain.FileIO.IDownloader> _downloader;
        private Mock<RestSharp.IRestClient> _restClient;
        private const string _downloadFolder = "Download";
        private const string _googleDriveAPIBaseUrl = "www.googleapis.com";
        private Infrastructure.FileIO.GoogleDriveList _googleDriveList;
        private List<Domain.FileIO.GoogleDriveItem> _googleDriveItems;

        [SetUp]
        public void Setup()
        {
            _downloader = new Mock<Domain.FileIO.IDownloader>();
            _restClient = new Mock<RestSharp.IRestClient>();

            _googleDrive = new Infrastructure.FileIO.GoogleDrive(
                                    _accessKey,
                                    _downloader.Object,
                                    _downloadFolder,                                    
                                    _googleDriveAPIBaseUrl,
                                    _restClient.Object);

            _googleDriveItems = new List<Domain.FileIO.GoogleDriveItem>();
            _googleDriveItems.Add(new Domain.FileIO.GoogleDriveItem() { Title = "file1.zip", DownloadUrl = "www.google.com" });
            _googleDriveItems.Add(new Domain.FileIO.GoogleDriveItem() { Title = "file2", DownloadUrl = "www.google.com" });

            _googleDriveList = new Infrastructure.FileIO.GoogleDriveList()
            {
                Items = _googleDriveItems
            };

            _restClient.Setup(r =>
                r.Execute<Infrastructure.FileIO.GoogleDriveList>(It.IsAny<RestSharp.IRestRequest>()))
                .Returns(
                new RestSharp.RestResponse<Infrastructure.FileIO.GoogleDriveList>()
                {
                    ResponseStatus = RestSharp.ResponseStatus.Completed,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = _googleDriveList
                });
        }

        #region GetListOfFiles
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [Test]
        public void GetListOfFiles_WhenPassEmptyGoogleDriveFolderAddress_ThrowsNullException(string googleDriveFolderAddress)
        {
            Assert.That(() =>
                _googleDrive.GetListOfFiles(googleDriveFolderAddress),
                Throws.ArgumentNullException);
        }

        [TestCase("httpAddress")]
        [Test]
        public void GetListOfFiles_WhenPassAddress_ShouldCallRestAPI(string googleDriveFolderAddress)
        {
            _googleDrive.GetListOfFiles(googleDriveFolderAddress);
            _restClient.Verify(r => r.Execute<Infrastructure.FileIO.GoogleDriveList>(It.IsAny<RestSharp.IRestRequest>()), Times.Once());
        }

        [Test]
        public void GetListOfFiles_WhenExistFiles_ShouldReturnAList()
        {
            var rst = _googleDrive.GetListOfFiles("httpAddress");
            Assert.That(rst, Is.Not.Null);
            Assert.That(rst, Is.TypeOf<List<Domain.FileIO.GoogleDriveItem>>());
        }

        [TestCase("zip")]
        [TestCase("txt")]
        [Test]
        public void GetListOfFiles_WhenPassingExtensionFilter_ShouldReturnJustRequestedExtension(string extensionFilter)
        {
            IEnumerable<Domain.FileIO.GoogleDriveItem> rst = 
                _googleDrive.GetListOfFiles("httpAddress", extensionFilter);
            
            Assert.That(
                    rst.Count() == 0 || rst.All(f => f.Title.EndsWith(extensionFilter)), 
                    Is.True);
        }
        #endregion
    }
}
