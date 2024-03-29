using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MaerskChallenge.Services;
using MaerskChallenge.Model;

namespace UnitTests
{
    public class SortingBackgroundServiceTest
    {
        private readonly SortingBackgroundService _sortingBackgroundService;

        public SortingBackgroundServiceTest()
        {
            var loggerMock = new Mock<ILogger<SortingBackgroundService>>();
            var sortingService = new Mock<SortingService<int>>();

        _sortingBackgroundService = new SortingBackgroundService(null, sortingService.Object, loggerMock.Object);
        }

        [Theory]
        [InlineData(new [] { 1 }, new [] { 1 })]
        [InlineData(new [] { 1, 2, 3 }, new [] { 1, 2, 3 })]
        [InlineData(new [] { 5, 4, 3, 2, 1 }, new [] { 1, 2, 3, 4, 5 })]
        public void SortInputArray(int[] inputArray, int[] expectedArray)
        {
            Job job = new Job
            {
                InputArray = inputArray
            };

            _sortingBackgroundService.ExecuteSortingAsync(job);

            job.SortedArray.Should().Equal(expectedArray);
        }

        [Fact]
        public void JobIsCompletedWhenSortingDone()
        {
            Job job = new Job
            {
                InputArray = new[] { 1, 2, 3 }
            };

            _sortingBackgroundService.ExecuteSortingAsync(job);

            job.Status.Should().Be(JobStatus.Completed);
        }

        [Fact]
        public void JobIsFailedWhenException()
        {
            Job job = new Job();

            _sortingBackgroundService.ExecuteSortingAsync(job);

            job.Status.Should().Be(JobStatus.Failed);
        }
    }
}