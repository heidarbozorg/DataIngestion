using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static string _rootAddress = Environment.CurrentDirectory + "\\";

		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;
		static List<Domain.Entities.CollectionMatch> _collectionMatches;
		static List<Domain.Entities.Collection> _collections;

		static void Unzip()
        {
			Console.WriteLine("2- Unzip files");
			var unzipFolder = _rootAddress + "Unzip\\";
			var downloadFolder = _rootAddress + "Downloads\\";
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
				System.IO.Compression.ZipFile.ExtractToDirectory(downloadFolder + z,
							unzipFolder, true);
			}
		}

		static void ReadFiles()
        {
            Console.WriteLine("3- Read unzip files");
            Console.WriteLine("\r --> Read artist file.");
			_artists = new Infrastructure.FileIO.ArtistRepository(_rootAddress + "Unzip\\artist").GetAll();

			Console.WriteLine("\r --> Read artist_collection file.");
			_artistCollections = new Infrastructure.FileIO.ArtistCollectionRepository(_rootAddress + "Unzip\\artist_collection").GetAll();

			Console.WriteLine("\r --> Read collection_match file.");
			_collectionMatches = new Infrastructure.FileIO.CollectionMatchRepository(_rootAddress + "Unzip\\collection_match").GetAll();

			Console.WriteLine("\r --> Read collection file.");
			_collections = new Infrastructure.FileIO.CollectionRepository(_rootAddress + "Unzip\\collection").GetAll();
        }

		static void InsertIntoElasticSearch()
        {
			Console.WriteLine("4- Insert into ElasticSearch.");
			var settings = new Settings();
			using (var unitOfWork = new Infrastructure.UnitOfWork(settings.ElasticSearchUrl))
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
			// Todo: 
			// 1- Download from goodle drive

			Unzip();
			ReadFiles();
			InsertIntoElasticSearch();

			Console.WriteLine("\nDone!");
		}
	}
}