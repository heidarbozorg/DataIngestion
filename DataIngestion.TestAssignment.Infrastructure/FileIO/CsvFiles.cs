using System;
using System.Collections.Generic;
using System.IO;

namespace DataIngestion.TestAssignment.Infrastructure.FileIO
{
    public abstract class CsvFiles<TEntity> : Domain.FileIO.ICsvFiles<TEntity> where TEntity : class
    {
        private const string _fieldSeparator = "\u0001";
        private const string _dirtyString = "\u0002";

        private readonly string _fileAddress;

        protected CsvFiles(string fileAddress)
        {
            if (!System.IO.File.Exists(fileAddress))
                throw new FileNotFoundException();

            _fileAddress = fileAddress;
        }

        public List<TEntity> GetAll()
        {
            var fileName = Path.GetFileName(_fileAddress);
            Console.WriteLine("\r --> Read {0} file.", fileName);
            var rst = new List<TEntity>();

            using (var reader = new StreamReader(File.OpenRead(_fileAddress)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var obj = Parse(line);
                    if (obj != null)
                        rst.Add(obj);
                }
            }

            return rst;
        }

        protected abstract TEntity Parse(string[] splitedStr);

        private TEntity Parse(string line)
        {
            try
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    return null;

                var sp = line.Replace(_dirtyString, "").Split(_fieldSeparator);
                return Parse(sp);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
