using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using WuhEatz.DenpaDB.Models;

namespace WuhEatz.DenpaDB.Contexts
{
  public class StartupContext : DbContext
  {
    public StartupContext(DbContextOptions options) : base(options) { }

    //public DbSet<ArchiveObject> ArchiveObjects { get; init; }
    public DbSet<UserProfile> Profiles { get; init; }
    public DbSet<Session> Sessions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserProfile>()
        .HasMany(up => up.Sessions)
        .WithOne(s => s.Owner);
    }

    public static StartupContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<StartupContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
  }
}
