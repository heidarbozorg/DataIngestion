using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;
		static List<Domain.Entities.CollectionMatch> _collectionMatches;
		static List<Domain.Entities.Collection> _collections;
		static List<Domain.Entities.NoSQL.Collection> _noSQLcollections;

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

		static void CreateElasticSearchDataSource()
        {
			Console.WriteLine("\r --> Create ElasticSearch DataSource.");
			var a = new Domain.Entities.NoSQL.Artist(935585671, "Anmol Dhaliwal");
			var artists = new List<Domain.Entities.NoSQL.Artist>();
			artists.Add(a);
			
			var c = new Domain.Entities.NoSQL.Collection()
			{
				id = 1255407551,
				name = "Nishana - Single",
				url = "http://ms.com/album/nishana-single/1255407551?uo=5",
				upc = "191061793557", // found in CollectionMatch file
				releaseDate = Convert.ToDateTime("2017-06-10T00:00:00"),
				isCompilation = false,
				label = "Aark Records",
				imageUrl = "http://img.com/image/thumb/Music117/v4/92/b8/51/92b85100-13c8-8fa4-0856-bb27276fdf87/191061793557.jpg/170x170bb.jpg",
				artists = artists
			};

			_noSQLcollections = new List<Domain.Entities.NoSQL.Collection>();
			_noSQLcollections.Add(c);
		}

		static void InsertIntoElasticSearch()
        {
			Console.WriteLine("4- Insert into ElasticSearch.");
			CreateElasticSearchDataSource();
			Console.WriteLine("\r --> Insert {0} records ...", _noSQLcollections.Count);
		}

		static void Main(string[] args)
		{
			// Todo: 
			// 1- Download from goodle drive
			// 2- Unzip files
			// 4- Insert into ElasticSearch

			ReadFiles();
			InsertIntoElasticSearch();

			Console.WriteLine("Done!");
		}
	}
}
