namespace MaerskChallenge.Model
{
    public interface IJobRepository
    {
        public Task<IList<Job>> GetJobs();
        public Task AddJob(Job job);
        public Task<Job> GetJob(Guid id);

    }
}
