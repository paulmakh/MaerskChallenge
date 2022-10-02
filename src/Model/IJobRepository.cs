namespace MaerskChallenge.Model
{
    public interface IJobRepository
    {
        public Task<IList<Job>> GetJobsAsync();
        public Task AddJobAsync(Job job);
        public Task<Job> GetJobAsync(Guid id);

    }
}
