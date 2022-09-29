namespace MaerskChallenge.Model
{
    public class JobRepository : IJobRepository
    {
        private IList<Job> _jobs;

        public JobRepository()
        {
            _jobs = new List<Job>();
        }

        async public Task<IList<Job>> GetJobs()
        {
            return await Task.Run<IList<Job>>(() => _jobs);
        }

        async public Task AddJob(Job job)
        {
            await Task.Run(() => _jobs.Add(job));
        }

        async public Task<Job> GetJob(Guid id)
        {
            return await Task.Run<Job>(() => _jobs.FirstOrDefault(x => x.Id == id));
        }
    }
}
