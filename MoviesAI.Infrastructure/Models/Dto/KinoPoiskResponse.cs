﻿using System.Text.Json.Serialization;

namespace MoviesAI.Infrastructure.Models.Dto;

public class KinoPoiskResponse
{
    [JsonPropertyName("docs")]
    public List<Doc> Docs { get; set; }
    [JsonPropertyName("total")]
    public int Total { get; set; }
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("pages")]
    public int Pages { get; set; }
}