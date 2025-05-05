using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MoviesAI.Domain.Entities;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MoviesAI.Infrastructure;

public class DataBaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<JobResultEntity> JobResults { get; set; }
    public DbSet<UserPreferenceEntity> UserPreferences { get; set; }
    
    

    public DataBaseContext()
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>()
            .Property(e => e.Genres)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<string>?>(x));

        modelBuilder.Entity<MovieEntity>()
            .Property(e => e.Actors)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<string>?>(x));

        modelBuilder.Entity<MovieEntity>()
            .Property(e => e.CreatedCountries)
            .HasColumnType("jsonb")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<string>?>(x));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdgb;Username=postgres;Password=0000");
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
}