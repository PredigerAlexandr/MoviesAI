namespace MoviesAI.Domain.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Title { get; set; }
}