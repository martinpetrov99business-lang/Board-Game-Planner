using BoardGamePlanner.Common;
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
            builder.Entity<GameNight>()
                 .HasOne(g => g.Creator)
                 .WithMany()
                 .HasForeignKey(g => g.CreatorId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Venue>().HasData(
             new Venue { Id = 1, Name = "The Dice Tower Café", Address = "12 Main Street, Downtown", Capacity = 30 },
             new Venue { Id = 2, Name = "Board Game Paradise", Address = "25 Oak Street, City Center", Capacity = 50 },
             new Venue { Id = 3, Name = "The Gaming Hub", Address = "8 River Road, West District", Capacity = 40 },
             new Venue { Id = 4, Name = "Meeple House", Address = "17 Park Avenue, East District", Capacity = 25 }
            );

            builder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "Catan",
                    Description = "Trade and build settlements on a modular island.",
                    MinPlayers = 3,
                    MaxPlayers = 4,
                    PlayTimeMinutes = 90,
                    Complexity = Complexity.Medium
                },
                new Game
                {
                    Id = 2,
                    Title = "Ticket to Ride",
                    Description = "Collect train cards and connect cities by building railway routes across the map.",
                    MinPlayers = 2,
                    MaxPlayers = 5,
                    PlayTimeMinutes = 60,
                    Complexity = Complexity.Light,
                    ImageUrl = null
                },
                new Game
                {
                    Id = 3,
                    Title = "Carcassonne",
                    Description = "Build a shared landscape with tiles and place followers to score points.",
                    MinPlayers = 2,
                    MaxPlayers = 5,
                    PlayTimeMinutes = 45,
                    Complexity = Complexity.Light,
                    ImageUrl = null
                },
                new Game
                {
                    Id = 4,
                    Title = "Pandemic",
                    Description = "Work together as a team of specialists to contain disease outbreaks around the world.",
                    MinPlayers = 2,
                    MaxPlayers = 4,
                    PlayTimeMinutes = 60,
                    Complexity = Complexity.Medium,
                    ImageUrl = null
                },
                new Game
                {
                    Id = 5,
                    Title = "Terraforming Mars",
                    Description = "Manage resources and develop projects to make Mars suitable for human life.",
                    MinPlayers = 1,
                    MaxPlayers = 5,
                    PlayTimeMinutes = 120,
                    Complexity = Complexity.Heavy,
                    ImageUrl = null
                },
                new Game
                {
                    Id = 6,
                    Title = "Chess",
                    Description = "Compete in a strategic battle by moving pieces across the board to checkmate your opponent.",
                    MinPlayers = 2,
                    MaxPlayers = 2,
                    PlayTimeMinutes = 30,
                    Complexity = Complexity.Medium,
                    ImageUrl = null
                },
                new Game
                {
                    Id = 7,
                    Title = "Gloomhaven",
                    Description = "Explore a fantasy world through tactical cooperative adventures and evolving campaigns.",
                    MinPlayers = 1,
                    MaxPlayers = 4,
                    PlayTimeMinutes = 120,
                    Complexity = Complexity.Heavy,
                    ImageUrl = null
                }

            );
        }

    }
}