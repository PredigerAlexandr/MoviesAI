using System.ComponentModel.DataAnnotations;

namespace MoviesAI.Domain.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Age { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    
}