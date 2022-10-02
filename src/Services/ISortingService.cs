namespace MaerskChallenge.Services
{
    public interface ISortingService<T>
    {
        public IEnumerable<T> Sort(IEnumerable<T> unsortedList);
    }
}
