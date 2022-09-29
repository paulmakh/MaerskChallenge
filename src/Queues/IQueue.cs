namespace MaerskChallenge.Queue
{
    public interface IQueue<T>
    {
        void Enqueue(T item);

        Task<T> Dequeue(CancellationToken cancellationToken);
    }
}
