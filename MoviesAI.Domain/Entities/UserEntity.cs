using System.ComponentModel.DataAnnotations;

namespace MoviesAI.Domain.Entities;

/// <summary>
/// 
/// </summary>
public class UserEntity
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Guid>? FavoriteMovieIds { get; set; } = new();
}