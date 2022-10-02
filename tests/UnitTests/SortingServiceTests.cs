using Xunit;
using FluentAssertions;
using MaerskChallenge.Services;

namespace UnitTests
{
    public class SortingServiceTests
    {
        private readonly SortingService<int> _sortingService;

        public SortingServiceTests()
        {
            _sortingService = new SortingService<int>();
        }

        [Theory]
        [InlineData(new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 })]
        [InlineData(new[] { 5, 4, 3, 2, 1 }, new[] { 1, 2, 3, 4, 5 })]
        public void SortTest(int[] inputArray, int[] expectedArray)
        {
            var sortedArray = _sortingService.Sort(inputArray);

            sortedArray.Should().Equal(expectedArray);
        }

    }
}
