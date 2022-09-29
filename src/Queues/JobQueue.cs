using System.Threading.Channels;

namespace MaerskChallenge.Queue
{
    public class JobQueue<T> : IQueue<T>
    {
        private readonly Channel<T> _channel;
        private readonly ILogger<JobQueue<T>> _logger;

        public JobQueue(ILogger<JobQueue<T>> logger)
        {
            _logger = logger;
            _channel = Channel.CreateUnbounded<T>();
        }

        public void Enqueue(T item)
        {
            _logger.LogTrace($"JobQueue.Enqueue");
            _channel.Writer.TryWrite(item);
        }

        public async Task<T> Dequeue(CancellationToken cancellationToken)
        {
            _logger.LogTrace($"JobQueue.Dequeue");
            return await _channel.Reader.ReadAsync();
        }

    }
}
