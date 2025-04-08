using Microsoft.EntityFrameworkCore;
using MoviesAI.Domain.Entities;

namespace MoviesAI.Infrastructure;

public class DataBaseContext:DbContext
{
    public DbSet<UserEntity> Users;
    public DbSet<MovieEntity> Movies;
    public DbSet<JobResultEntity> JobResults;
    
    public DataBaseContext()
    {
        Database.Migrate();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdgb;Username=postgres;Password=0000");
    }
}