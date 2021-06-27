using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace DataIngestion.TestAssignment.Infrastructure.UnitTests.NoSQL
{
    [TestFixture]
    class CollectionRepositoryTests
    {
        private Mock<IElasticClient> _client;
        private Infrastructure.NoSQL.CollectionRepository _collectionRepository;
        
        private List<Domain.Entities.Artist> _artists;
        private List<Domain.Entities.ArtistCollection> _artistCollections;
        private List<Domain.Entities.Collection> _collections;
        private List<Domain.Entities.CollectionMatch> _collectionMatches;


        private void SetupMockData()
        {
            _artists = new List<Domain.Entities.Artist>();
            _artists.Add(new Domain.Entities.Artist() { 
                ArtistId = 1,
                Name = "Name"
            });

            _artistCollections = new List<Domain.Entities.ArtistCollection>();
            _artistCollections.Add(new Domain.Entities.ArtistCollection()
            {
                ArtistId = 1,
                CollectionId = 1
            });

            _collections = new List<Domain.Entities.Collection>();
            _collections.Add(new Domain.Entities.Collection()
            {
                CollectionId = 1                
            });

            _collectionMatches = new List<Domain.Entities.CollectionMatch>();
            _collectionMatches.Add(new Domain.Entities.CollectionMatch()
            {
                CollectionId = 1
            });
        }


        [SetUp]
        public void Setup()
        {
            _client = new Mock<IElasticClient>();
            _collectionRepository = new Infrastructure.NoSQL.CollectionRepository(_client.Object);
            SetupMockData();
        }

        [Test]
        public void AddRange_WhenPassingNull_ThrowsNullException()
        {
            Assert.That(() =>
                _collectionRepository.AddRange(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetCollection_WhenPassingNull_ThrowsNullException()
        {            
            Assert.That(() =>
                _collectionRepository.GetCollection(null, _artistCollections, _collections, _collectionMatches),
                Throws.ArgumentNullException);

            Assert.That(() =>
                _collectionRepository.GetCollection(_artists, null, _collections, _collectionMatches),
                Throws.ArgumentNullException);

            Assert.That(() =>
                _collectionRepository.GetCollection(_artists, _artistCollections, null, _collectionMatches),
                Throws.ArgumentNullException);

            Assert.That(() =>
                _collectionRepository.GetCollection(_artists, _artistCollections, _collections, null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetCollection_WhenPassingData_ReturnACollection()
        {
            var rst = _collectionRepository.GetCollection(
                                _artists, _artistCollections, 
                                _collections, _collectionMatches).ToList();

            Assert.That(rst.Count, Is.GreaterThanOrEqualTo(1));

            Assert.That(rst.Exists(c => c.id == _collections[0].CollectionId
                    && c.artists.ToList().Exists(a => a.id == _artists[0].ArtistId)), 
                Is.True);            
        }
    }
}
