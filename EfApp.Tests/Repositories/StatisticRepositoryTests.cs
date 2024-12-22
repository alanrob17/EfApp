using static EfApp.Models.Record;
using EfApp.Repositories;
using EfApp.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using EfApp.Models;

namespace EfApp.Tests.Repositories
{
    public class StatisticRepositoryTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<RecordService> _mockRecordService;
        private readonly StatisticRepository _repository;

        public StatisticRepositoryTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockRecordService = new Mock<RecordService>();
            _repository = new StatisticRepository(_mockContext.Object, _mockRecordService.Object);
        }

        [Fact]
        public async Task GetStatistics_ShouldReturnCorrectStatistics()
        {
            // Arrange
            var mockRecords = new List<Models.Record>
            {
                new Models.Record { ArtistId = 101, Name = "Rockin' the Bass", Field = "Rock", Recorded = 2019, Label = "Wibble", Pressing = "Aus", Rating = "****", Discs = 1, Media = "CD", Bought = new DateTime(2022, 1, 16), Cost = 29.95m, CoverName = "rockin_the_bass.jpg", Review = "This is James's first album."},
                new Models.Record { ArtistId = 102, Name = "Jazz Vibes", Field = "Jazz", Recorded = 2020, Label = "Jazzify", Pressing = "US", Rating = "*****", Discs = 2, Media = "Vinyl", Bought = new DateTime(2021, 5, 10), Cost = 45.00m, CoverName = "jazz_vibes.jpg", Review = "A smooth and relaxing jazz album."},
                new Models.Record { ArtistId = 103, Name = "Classical Masterpieces", Field = "Classical", Recorded = 2018, Label = "Classics", Pressing = "EU", Rating = "****", Discs = 3, Media = "CD", Bought = new DateTime(2020, 11, 5), Cost = 39.99m, CoverName = "classical_masterpieces.jpg", Review = "A collection of timeless classical pieces."},
                new Models.Record { ArtistId = 104, Name = "Pop Hits", Field = "Pop", Recorded = 2021, Label = "PopWorld", Pressing = "UK", Rating = "***", Discs = 1, Media = "CD", Bought = new DateTime(2022, 3, 22), Cost = 19.99m, CoverName = "pop_hits.jpg", Review = "A compilation of the latest pop hits."},
                new Models.Record{ ArtistId = 105, Name = "Electronic Beats", Field = "Electronic", Recorded = 2022, Label = "Electro", Pressing = "JP", Rating = "*****", Discs = 2, Media = "Vinyl", Bought = new DateTime(2022, 7, 15), Cost = 49.99m, CoverName = "electronic_beats.jpg", Review = "An energetic and vibrant electronic album."}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Models.Record>>();
            mockDbSet.As<IQueryable<Models.Record>>().Setup(m => m.Provider).Returns(mockRecords.Provider);
            mockDbSet.As<IQueryable<Models.Record>>().Setup(m => m.Expression).Returns(mockRecords.Expression);
            mockDbSet.As<IQueryable<Models.Record>>().Setup(m => m.ElementType).Returns(mockRecords.ElementType);
            mockDbSet.As<IQueryable<Models.Record>>().Setup(m => m.GetEnumerator()).Returns(mockRecords.GetEnumerator());

            _mockContext.Setup(c => c.Set<Models.Record>()).Returns(mockDbSet.Object);
            _mockRecordService.Setup(s => s.GetTotalCostByYearBoughtAsync(It.IsAny<int>())).ReturnsAsync(100);
            _mockRecordService.Setup(s => s.GetTotalDiscsByYearBoughtAsync(It.IsAny<int>())).ReturnsAsync(10);

            // Act
            var result = await _repository.GetStatistics();

            // Assert
            Assert.NotNull(result);

            // Assuming the GetStatistics method calculates these statistics based on the provided records
            Assert.Equal(1, result.RockDiscs); // Only 1 Rock record in mock data
            Assert.Equal(0, result.FolkDiscs); // No Folk records in mock data
            Assert.Equal(3, result.TotalCDs); // There are 3 CDs in mock data
            Assert.Equal(100, result.TotalCost); // Mocked total cost from RecordService        }
        }
    }
}
