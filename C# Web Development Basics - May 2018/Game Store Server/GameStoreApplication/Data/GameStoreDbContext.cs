using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GameStoreApplication.Data.Models;

namespace WebServer.GameStoreApplication.Data
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.
                UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=GameStore;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserGame>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            builder.Entity<UserGame>()
                .HasOne(u => u.User)
                .WithMany(ug => ug.UserGames)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserGame>()
                .HasOne(g => g.Game)
                .WithMany(ug => ug.UserGames)
                .HasForeignKey(g => g.GameId);
        }
    }
}
