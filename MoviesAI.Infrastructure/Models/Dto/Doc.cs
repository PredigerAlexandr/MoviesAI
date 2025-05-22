using System.Text.Json.Serialization;

namespace MoviesAI.Infrastructure.Models.Dto;

public class Doc
{
    [JsonPropertyName("id")] 
    public long Id { get; set; }
    
    [JsonPropertyName("createdAt")] 
    public DateTime CreatedAt { get; set; }
    
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("genres")] 
    public List<ResponseListModel> Genres { get; set; }
    
    [JsonPropertyName("countries")] 
    public List<ResponseListModel> Countries { get; set; }
    
    [JsonPropertyName("year")] 
    public int? Year { get; set; }
    
    [JsonPropertyName("persons")] 
    public List<ResponseListModel>? Persons { get; set; }

    [JsonPropertyName("poster")]
    public Poster Poster { get; set; }
}