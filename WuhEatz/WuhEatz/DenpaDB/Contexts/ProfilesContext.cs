﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using WuhEatz.DenpaDB.Models;

namespace WuhEatz.DenpaDB.Contexts
{
  public class ProfilesContext : DbContext
  {
    public ProfilesContext(DbContextOptions options) : base(options) { }

    public DbSet<UserProfile> Profiles { get; init; }
    public DbSet<Session> Sessions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Session>()
        .HasOne(s => s.Owner)
        .WithMany(up => up.Sessions);
    }

    public static ProfilesContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<ProfilesContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
  }
}