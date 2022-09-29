using MaerskChallenge.Model;
using MaerskChallenge.Queue;

namespace MaerskChallenge.Services
{
    public class SortingBackgroundService : BackgroundService
    {
        private readonly IQueue<Job> _jobQueue;
        private readonly ILogger<SortingBackgroundService> _logger;

        public SortingBackgroundService(
            IQueue<Job> jobQueue, 
            ILogger<SortingBackgroundService> logger)
        {
            _jobQueue = jobQueue;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _jobQueue.Dequeue(stoppingToken);

                _logger.LogInformation($"Dequeued. Job id: ${job.Id}");

                await Task.Run(() => ExecuteSorting(job));
            }
        }

        public void ExecuteSorting(Job job)
        {
            try
            {
                job.SortedArray = job.InputArray.OrderBy(i => i);
                job.Duration = DateTime.UtcNow - job.Timestamp;
                job.Status = JobStatus.Completed;
                _logger.LogInformation($"Sorted successfully. Job id: ${job.Id}");
            }
            catch(Exception ex)
            {
                job.Status = JobStatus.Failed;
                _logger.LogError(ex, $"Exception during sorting array. Job id: ${job.Id}");
            }

        }
    }

}
