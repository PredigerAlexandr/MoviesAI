namespace MoviesAI.Domain.Entities;

public class UserPreferenceEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public string Preference { get; set; }
    public List<Guid> RatedFilmIds { get; set; }
    public List<MovieEntity> RatedFilms { get; set; }
}