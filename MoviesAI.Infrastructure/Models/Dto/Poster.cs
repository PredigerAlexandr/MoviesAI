using System.Text.Json.Serialization;

namespace MoviesAI.Infrastructure.Models.Dto;

public class Poster
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("previewUrl")]
    public string PreviewUrl { get; set; }
}