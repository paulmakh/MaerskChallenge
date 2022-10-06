using MaerskChallenge.Model;
using MaerskChallenge.Queue;

namespace MaerskChallenge.Services
{
    public class SortingBackgroundService : BackgroundService
    {
        private readonly IQueue<Job> _jobQueue;
        private readonly ISortingService<int> _sortingService;
        private readonly ILogger<SortingBackgroundService> _logger;

        public SortingBackgroundService(
            IQueue<Job> jobQueue,
            ISortingService<int> sortingService,
            ILogger<SortingBackgroundService> logger)
        {
            _jobQueue = jobQueue;
            _sortingService = sortingService;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _jobQueue.Dequeue(stoppingToken);

                _logger.LogInformation($"Dequeued. Job id: ${job.Id}");

                ExecuteSortingAsync(job);
            }
        }

        public void ExecuteSortingAsync(Job job)
        {
            try
            {
                job.SortedArray = _sortingService.Sort(job.InputArray);
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
