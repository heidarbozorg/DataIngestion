using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;
		static List<Domain.Entities.CollectionMatch> _collectionMatches;
		static List<Domain.Entities.Collection> _collections;

		static void ReadFiles()
        {
            Console.WriteLine("3- Read unzip files");
			var rootAddress = Environment.CurrentDirectory + "\\";
            Console.WriteLine("\r --> Read artist file.");
			_artists = new Infrastructure.FileIO.ArtistRepository(rootAddress + "Unzip\\artist").GetAll();

			Console.WriteLine("\r --> Read artist_collection file.");
			_artistCollections = new Infrastructure.FileIO.ArtistCollectionRepository(rootAddress + "Unzip\\artist_collection").GetAll();

			Console.WriteLine("\r --> Read collection_match file.");
			_collectionMatches = new Infrastructure.FileIO.CollectionMatchRepository(rootAddress + "Unzip\\collection_match").GetAll();

			Console.WriteLine("\r --> Read collection file.");
			_collections = new Infrastructure.FileIO.CollectionRepository(rootAddress + "Unzip\\collection").GetAll();
        }

		static void InsertIntoElasticSearch()
        {
			Console.WriteLine("4- Insert into ElasticSearch.");
			var elasticUrl = "http://localhost:9200";
			using (var unitOfWork = new Infrastructure.UnitOfWork(elasticUrl))
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
			// 2- Unzip files

			ReadFiles();
			InsertIntoElasticSearch();

			Console.WriteLine("\nDone!");
		}
	}
}
