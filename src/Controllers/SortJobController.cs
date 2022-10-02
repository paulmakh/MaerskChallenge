using Microsoft.AspNetCore.Mvc;

using MaerskChallenge.Model;
using MaerskChallenge.Queue;

namespace MaerskChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortJobController : ControllerBase
    {
        private readonly IQueue<Job> _jobQueue;
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<SortJobController> _logger;

        public SortJobController(
            IQueue<Job> jobQueue, 
            IJobRepository jobRepository,
            ILogger<SortJobController> logger)
        {
            _jobQueue = jobQueue;
            _jobRepository = jobRepository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve an overview of all jobs (both pending and completed)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        async public Task<IEnumerable<Job>> Get()
        {
            return await _jobRepository.GetJobsAsync();
        }

        /// <summary>
        /// Enqueue a new job
        /// </summary>
        /// <param name="value">Unsorted array of numbers</param>
        /// <returns>New enqueued job</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        async public Task<ActionResult<Job>> Enqueue(int[] value)
        {
            if (value == null || value.Length == 0)
            {
                return BadRequest("Input array is empty");
            }

            var job = new Job
            {
                Id = Guid.NewGuid(),
                Status = JobStatus.Pending,
                Timestamp = DateTime.UtcNow,
                InputArray = value
            };

            await _jobRepository.AddJobAsync(job);
            _jobQueue.Enqueue(job);

            _logger.LogInformation($"Enqueued. Job id: ${job.Id}");

            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        /// <summary>
        /// Retrieve a specific job by its ID
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns>Specific Job</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        async public Task<ActionResult<Job>> GetById(Guid id)
        {
            var job = await _jobRepository.GetJobAsync(id);

            if (job == null)
            {
                return NotFound($"Cannot find a job with Id {id}.");
            }

            return job;
        }
    }
}
