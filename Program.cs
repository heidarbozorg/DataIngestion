using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static Settings _settings;

		static Domain.FileIO.IGoogleDrive _googleDrive;
		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;
		static List<Domain.Entities.CollectionMatch> _collectionMatches;
		static List<Domain.Entities.Collection> _collections;

		static void Download()
        {
			Console.WriteLine("1- Download from Google.Drive");
			_googleDrive = new Infrastructure.FileIO.GoogleDrive(
									_settings.GoogleDriveAccessKey,
									_settings.GoogleDriveBaseUrl);
			Console.WriteLine("\r --> Get files info");
			var filesInfo = _googleDrive.GetFilesInfo(_settings.GoogleDriveFolderAddress,
								".zip");
			foreach(var f in filesInfo)
            {
				Console.WriteLine("\r\r --> Download " + f.Title);
			}
		}

		static void Unzip()
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

		static void ReadFiles()
        {
            Console.WriteLine("3- Read unzip files");
            Console.WriteLine("\r --> Read artist file.");
			_artists = new Infrastructure.FileIO.ArtistRepository(_settings.UnzipFolder + "artist").GetAll();

			Console.WriteLine("\r --> Read artist_collection file.");
			_artistCollections = new Infrastructure.FileIO.ArtistCollectionRepository(_settings.UnzipFolder + "artist_collection").GetAll();

			Console.WriteLine("\r --> Read collection_match file.");
			_collectionMatches = new Infrastructure.FileIO.CollectionMatchRepository(_settings.UnzipFolder + "collection_match").GetAll();

			Console.WriteLine("\r --> Read collection file.");
			_collections = new Infrastructure.FileIO.CollectionRepository(_settings.UnzipFolder + "collection").GetAll();
        }

		static void InsertIntoElasticSearch()
        {
			Console.WriteLine("4- Insert into ElasticSearch.");			
			using (var unitOfWork = new Infrastructure.UnitOfWork(_settings.ElasticSearchUrl))
			{
				Console.WriteLine("\r --> Create ElasticSearch DataSource.");
				var collections = unitOfWork.Collections.GetCollection(
									_artists,
									_artistCollections,
									_collections,
									_collectionMatches
									);

				unitOfWork.Collections.AddRange(collections);
			}
		}

		static void Main(string[] args)
		{
			_settings = new Settings();

			Download();
			Unzip();
			ReadFiles();
			InsertIntoElasticSearch();

			Console.WriteLine("\nDone!");
		}
	}
}