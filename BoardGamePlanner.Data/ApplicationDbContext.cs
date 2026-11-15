using BoardGamePlanner.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardGamePlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; } = null!;

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<GameNight> GameNights { get; set; } = null!;

        public DbSet<GameNightGame> GameNightGames { get; set; } = null!;

        public DbSet<GameNightSignup> GameNightSignups { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameNightGame>()
                .HasKey(x => new { x.GameNightId, x.GameId });

            builder.Entity<GameNightSignup>()
                .HasKey(x => new { x.GameNightId, x.UserId });

            
            builder.Entity<GameNight>()
                .HasOne(x => x.Venue)
                .WithMany(x => x.GameNights)
                .HasForeignKey(x => x.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.Entity<GameNightGame>()
                .HasOne(x => x.GameNight)
                .WithMany(x => x.GameNightGames)
                .HasForeignKey(x => x.GameNightId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<GameNightGame>()
                .HasOne(x => x.Game)
                .WithMany(x => x.GameNightGames)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.Entity<GameNightSignup>()
                .HasOne(x => x.GameNight)
                .WithMany(x => x.Signups)
                .HasForeignKey(x => x.GameNightId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<GameNightSignup>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}