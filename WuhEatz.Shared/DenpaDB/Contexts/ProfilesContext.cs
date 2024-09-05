using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using WuhEatz.Shared.DenpaDB.Models;

namespace WuhEatz.Shared.DenpaDB.Contexts
{
  public class ProfilesContext : DbContext
  {
    public ProfilesContext(DbContextOptions options) : base(options) { }

    public DbSet<UserProfile> Profiles { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<UserProfile>()
        .HasMany(up => up.Sessions)
        .WithOne(s => s.Owner);

      modelBuilder.Entity<Session>()
        .HasOne(s => s.Owner)
        .WithMany(up => up.Sessions)
        .HasForeignKey(x=> x.Owner_id);

      modelBuilder.Entity<Session>()
        .Navigation(s => s.Owner)
        .UsePropertyAccessMode(PropertyAccessMode.Property)
        .IsRequired();
    }

    public static ProfilesContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<ProfilesContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
  }
}