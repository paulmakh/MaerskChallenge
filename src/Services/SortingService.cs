namespace MaerskChallenge.Services
{
    public class SortingService<T> : ISortingService<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> unsortedList)
        {
            return unsortedList.OrderBy(i => i);
        }
    }
}
