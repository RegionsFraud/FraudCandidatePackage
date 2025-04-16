namespace MicroService.Commands
{
    public class ParallelAsync
    {
        public int DegreeOfParallelism { get; set; } = 3;
        public ParallelOptions Options(CancellationToken? token = null, TaskScheduler scheduler = null)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = DegreeOfParallelism };
            if (token.HasValue) options.CancellationToken = token.Value;
            if (scheduler != null) options.TaskScheduler = scheduler;
            return options;
        }
    }
}
