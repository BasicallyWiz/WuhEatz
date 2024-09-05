using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuhEatz.Shared.DenpaDB.Models;

namespace WuhEatz.Shared.DenpaDB.Contexts
{
  public class LoggingContext : DbContext
  {
    public LoggingContext(DbContextOptions options) : base(options) { }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Log>()
        .HasKey(l => l._id);

      modelBuilder.Entity<Log>()
        .HasOne(l => l.LoggedUser);

      modelBuilder.Entity<Log>()
        .Navigation(l => l.LoggedUser)
        .EnableLazyLoading(false)
        .IsRequired();

      base.OnModelCreating(modelBuilder);
    }

    public static LoggingContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<LoggingContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
  }
}
