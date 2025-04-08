namespace MoviesAI.Domain.Entities;

public class JobResultEntity
{
    public Guid Id { get; set; }
    public DateTime Started { get; set; }
    public DateTime ExecutionTime { get; set; }
    public StatusJob Status { get; set; }
}