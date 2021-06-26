using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static List<Domain.Entities.Artist> _artists;
		static List<Domain.Entities.ArtistCollection> _artistCollections;

		static void ReadFiles()
        {
			var rootAddress = Environment.CurrentDirectory + "\\";
			_artists = new Infrastructure.FileIO.ArtistRepository(rootAddress + "Unzip\\artist").GetAll();
			_artistCollections = new Infrastructure.FileIO.ArtistCollectionRepository(rootAddress + "Unzip\\artist_collection").GetAll();
        }

		static void Main(string[] args)
		{
			// Todo: 
			// 1- Download from goodle drive
			// 2- Unzip files
			// 3- Read unzip files
			// 4- Insert into ElasticSearch

			ReadFiles();

			Console.WriteLine("Hello World!");
		}
	}
}
