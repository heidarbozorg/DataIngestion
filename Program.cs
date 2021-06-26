using System;
using System.Collections.Generic;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		static Domain.Repositories.ICsvFiles<Domain.Entities.Artist> _artistCsvFile;
		static List<Domain.Entities.Artist> _artists;

		static void ReadFiles()
        {
			var rootAddress = Environment.CurrentDirectory + "\\";
			_artistCsvFile = new Infrastructure.FileIO.Artist(rootAddress + "Unzip\\artist");
			_artists = _artistCsvFile.GetAll();
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
