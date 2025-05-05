namespace MoviesAI.Domain.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public long ExternalId { get; set; }
    public string Title { get; set; }
    public List<string>? Genres { get; set; }
    public List<string>? Actors { get; set; }
    public int CreatedYear { get; set; }
    public List<string>? CreatedCountries { get; set; }
    public string ImageUrl { get; set; }
    
}