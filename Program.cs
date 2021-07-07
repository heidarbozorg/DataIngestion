using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
    class Program
	{
        #region Variables
        static Settings _settings;
		static Domain.FileIO.IGoogleDrive _googleDrive;
		static Domain.FileIO.IDownloader _downloader;
		static Domain.Repositories.IUnitOfWork _unitOfWork;

		static IEnumerable<Domain.Entities.Artist> _artists;
		static IEnumerable<Domain.Entities.ArtistCollection> _artistCollections;
		static IEnumerable<Domain.Entities.CollectionMatch> _collectionMatches;
		static IEnumerable<Domain.Entities.Collection> _collections;
        #endregion

        #region Methods
        static void Setup()
        {
			_settings = new Settings();
			
			_downloader = new Infrastructure.FileIO.Downloader();

			_googleDrive = new Infrastructure.FileIO.GoogleDrive(
									_settings.GoogleDriveAccessKey,
									_downloader,
									_settings.DownloadFolder,
									_settings.GoogleDriveAPIBaseUrl);

			
			_unitOfWork = new Infrastructure.UnitOfWork(_settings.ElasticSearchUrl);
		}

		static void Finish()
        {
			_downloader.Dispose();
			_unitOfWork.Dispose();
        }

		static void DownloadFromGoogleDrive()
        {
			Console.WriteLine("1- Download from Google Drive");

			var filesInfo = _googleDrive.GetListOfFiles(_settings.GoogleDriveFolderAddress, "zip");
			_googleDrive.Download(filesInfo);			
		}

		static void UnzipDownloadedFiles()
        {
			Console.WriteLine("2- Unzip files");
			var zipFiles = new string[] 
			{ 
				"artist.zip", 
				"artist_collection.zip" ,
				"collection.zip",
				"collection_match.zip"
			};

			foreach (var z in zipFiles)
			{
				Console.WriteLine("\r --> Unzip " + z);
				System.IO.Compression.ZipFile.ExtractToDirectory(
							_settings.DownloadFolder + z,
							_settings.UnzipFolder, true);
			}
		}

		static void ReadUnzipFiles()
        {
            Console.WriteLine("3- Read unzip files");

			_artists = new Infrastructure.FileIO.Artist(_settings.UnzipFolder + "artist")
								.GetAll();

			_artistCollections = new Infrastructure.FileIO.ArtistCollection(_settings.UnzipFolder + "artist_collection")
								.GetAll();

			_collectionMatches = new Infrastructure.FileIO.CollectionMatch(_settings.UnzipFolder + "collection_match")
								.GetAll();

			_collections = new Infrastructure.FileIO.Collection(_settings.UnzipFolder + "collection")
								.GetAll();
        }

		static void InsertIntoElasticSearch()
        {
			Console.WriteLine("4- Insert into ElasticSearch.");			
			Console.WriteLine("\r --> Create ElasticSearch DataSource.");
			var collections = _unitOfWork.Collections.GetCollection(
								_artists,
								_artistCollections,
								_collections,
								_collectionMatches
								);

			_unitOfWork.Collections.AddRange(collections);
		}
        #endregion

        static void Main(string[] args)
		{
			Setup();

			//DownloadFromGoogleDrive();
			UnzipDownloadedFiles();
			ReadUnzipFiles();
			InsertIntoElasticSearch();

			Finish();

			Console.WriteLine("\nDone!");
		}
	}
}