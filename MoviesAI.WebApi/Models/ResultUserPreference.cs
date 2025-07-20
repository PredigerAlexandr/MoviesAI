namespace MoviesAI.WebApi.Models;

public class ResultUserPreference
{
    public List<string> Actors { get; set; }
    public List<string> Genres { get; set; }
    public List<string> Countries { get; set; }
    public string Year { get; set; }
}