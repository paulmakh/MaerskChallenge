namespace MaerskChallenge.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Unique ID assigned by the application
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Job Status
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        /// Timestamp when was the job enqueued
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Duration how much time did it take to execute the job
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Input unsorted array
        /// </summary>
        public IEnumerable<int>? InputArray { get; set; }

        /// <summary>
        /// Output sorted array
        /// </summary>
        public IEnumerable<int>? SortedArray { get; set; }
    }
}
