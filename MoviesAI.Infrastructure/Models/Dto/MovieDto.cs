using System.Text.Json.Serialization;

namespace MoviesAI.Infrastructure.Models.Dto;

public class Doc
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}