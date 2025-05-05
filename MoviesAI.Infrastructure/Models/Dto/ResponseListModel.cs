using System.Text.Json.Serialization;

namespace MoviesAI.Infrastructure.Models.Dto;

public class ResponseListModel
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }
}