namespace MaerskChallenge.Services
{
    public interface ISortingService<T>
    {
        public Task<IEnumerable<T>> Sort(IEnumerable<T> unsortedList);
    }
}
