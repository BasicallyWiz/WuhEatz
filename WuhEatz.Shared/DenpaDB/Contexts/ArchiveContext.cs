using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using WuhEatz.Shared.DenpaDB.Models;

namespace WuhEatz.Shared.DenpaDB.Contexts
{
  public class ArchiveContext : DbContext
  {
    public ArchiveContext(DbContextOptions options) : base(options) { }

    public DbSet<ArchiveObject> ArchiveObjects { get; set; }
    public DbSet<ArchiveContributer> ArchiveContributers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public static ArchiveContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<ArchiveContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
  }
}
