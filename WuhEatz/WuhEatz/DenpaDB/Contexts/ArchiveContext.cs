using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using WuhEatz.DenpaDB.Models;

namespace WuhEatz.DenpaDB.Contexts
{
  public class ArchiveContext : DbContext
  {
    public ArchiveContext(DbContextOptions options) : base(options) { }

    public DbSet<ArchiveObject> ArchiveObjects { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<ArchiveObject>().ToCollection("archive-objects");
    }
  }
}
