namespace MaerskChallenge.Services
{
    public class SortingService<T> : ISortingService<T>
    {
        public async Task<IEnumerable<T>> Sort(IEnumerable<T> unsortedList)
        {
            return  await Task.Run(() => {
                return unsortedList.OrderBy(i => i);
            });
        }
    }
}
