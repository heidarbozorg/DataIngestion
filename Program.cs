﻿using System;
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

		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;
		static List<Domain.Entities.CollectionMatch> _collectionMatches;
		static List<Domain.Entities.Collection> _collections;
        #endregion

        #region Methods
        static void Setup()
        {
			_settings = new Settings();
			_googleDrive = new Infrastructure.FileIO.GoogleDrive(
									_settings.GoogleDriveAccessKey,
									_settings.GoogleDriveBaseUrl);

			_downloader = new Infrastructure.FileIO.Downloader();
			
			_unitOfWork = new Infrastructure.UnitOfWork(_settings.ElasticSearchUrl);
		}

		static void Finish()
        {
			_downloader.Dispose();
			_unitOfWork.Dispose();
        }

		static void Download()
        {
			Console.WriteLine("1- Download from Google Drive");

			var filesInfo = _googleDrive.GetFilesInfo(_settings.GoogleDriveFolderAddress, "zip");

			foreach (var f in filesInfo)
			{
				var destionationFileAddress = _settings.DownloadFolder + f.Title;
				_downloader.Download(f.DownloadUrl ?? f.AlternateLink,
										destionationFileAddress);
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
			_artists = new Infrastructure.FileIO.ArtistRepository(_settings.UnzipFolder + "artist").GetAll();
			_artistCollections = new Infrastructure.FileIO.ArtistCollectionRepository(_settings.UnzipFolder + "artist_collection").GetAll();
			_collectionMatches = new Infrastructure.FileIO.CollectionMatchRepository(_settings.UnzipFolder + "collection_match").GetAll();
			_collections = new Infrastructure.FileIO.CollectionRepository(_settings.UnzipFolder + "collection").GetAll();
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

			Download();
			Unzip();
			ReadFiles();
			InsertIntoElasticSearch();

			Finish();

			Console.WriteLine("\nDone!");
		}
	}
}