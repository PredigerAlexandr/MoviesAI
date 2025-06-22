using MoviesAI.Domain.Entities;

namespace MoviesAI.WebApi.Models;

public class FavoriteMoviesDto
{
    public MovieEntity[] Movies { get; set; }
    public PagingInfo  PagingInfo { get; set; }
}

public class PagingInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
}