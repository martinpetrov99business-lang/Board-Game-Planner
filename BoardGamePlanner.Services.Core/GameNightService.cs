using BoardGamePlanner.Data;
using BoardGamePlanner.Data.Models;
using BoardGamePlanner.Services.Core.Contracts;
using BoardGamePlanner.ViewModels.GameNights;
using Microsoft.EntityFrameworkCore;


namespace BoardGamePlanner.Services.Core
{
    public class GameNightService : IGameNightService
    {

        private readonly ApplicationDbContext dbContext;

        public GameNightService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> FitsVenueAsync(int venueId, int maxGuests)
        {
            int? capacity = await dbContext.Venues
                .Where(v => v.Id == venueId)
                .Select(v => (int?)v.Capacity)
                .FirstOrDefaultAsync();

            if (capacity == null)
            {
                return false;
            }

            return maxGuests <= capacity.Value;
        }
        public async Task<IEnumerable<GameNightIndexViewModel>> GetAllAsync(string? userId)
        {
            return await dbContext.GameNights
                .AsNoTracking()
                .OrderBy(g => g.Date)
                .Select(g => new GameNightIndexViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Date = g.Date,
                    VenueName = g.Venue.Name,
                    MaxGuests = g.MaxGuests,
                    SignedUpCount = g.Signups.Count,
                     IsJoined = userId != null &&
               g.Signups.Any(s => s.UserId == userId),
               IsCreator = userId != null && g.CreatorId == userId
                })
                .ToListAsync();
        }

        public async Task<GameNightDetailsViewModel?> GetDetailsAsync(int id, string? userId)
        {
            return await dbContext.GameNights
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GameNightDetailsViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Date = g.Date,
                    VenueName = g.Venue.Name,
                    VenueAddress = g.Venue.Address,
                    MaxGuests = g.MaxGuests,
                    Notes = g.Notes,
                     SignedUpCount = g.Signups.Count,
                    IsJoined = userId != null &&
               g.Signups.Any(s => s.UserId == userId),
                    IsCreator = userId != null && g.CreatorId == userId,

                    Games = g.GameNightGames
                .Select(x => new GameOptionViewModel
                 {
                      Id = x.Game.Id,
                      Title = x.Game.Title
                 })
                .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GameNightFormViewModel> GetCreateModelAsync()
        {
            return new GameNightFormViewModel
            {
                Date = DateTime.Today.AddDays(7).AddHours(19),
                Venues = await GetVenuesAsync(),
                AvailableGames = await GetGamesAsync()
            };
        }

        public async Task<int> CreateAsync(GameNightFormViewModel model, string userId)
        {
            GameNight gameNight = new GameNight
            {
                Title = model.Title,
                Date = model.Date,
                Notes = model.Notes,
                MaxGuests = model.MaxGuests,
                VenueId = model.VenueId,
                CreatorId = userId
            };

            List<int> validIds = await dbContext.Games
                .Where(g => model.SelectedGameIds.Contains(g.Id))
                .Select(g => g.Id)
                .ToListAsync();

            foreach (int gameId in validIds)
            {
                gameNight.GameNightGames.Add(new GameNightGame
                {
                    GameId = gameId
                });
            }

            await dbContext.GameNights.AddAsync(gameNight);
            await dbContext.SaveChangesAsync();

            return gameNight.Id;
        }

        public async Task<GameNightFormViewModel?> GetEditModelAsync(int id)
        {
            GameNight? gameNight = await dbContext.GameNights
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameNight == null)
            {
                return null;
            }

            return new GameNightFormViewModel
            {
                Id = gameNight.Id,
                Title = gameNight.Title,
                Date = gameNight.Date,
                Notes = gameNight.Notes,
                MaxGuests = gameNight.MaxGuests,
                VenueId = gameNight.VenueId,
                Venues = await GetVenuesAsync(),
                AvailableGames = await GetGamesAsync(),

                SelectedGameIds = await dbContext.GameNightGames
        .Where(x => x.GameNightId == id)
        .Select(x => x.GameId)
        .ToListAsync()
            };
        }

        public async Task<bool> UpdateAsync(GameNightFormViewModel model)
        {
            GameNight? gameNight = await dbContext.GameNights
        .Include(g => g.GameNightGames)
        .FirstOrDefaultAsync(g => g.Id == model.Id);

            if (gameNight == null)
            {
                return false;
            }

            gameNight.Title = model.Title;
            gameNight.Date = model.Date;
            gameNight.Notes = model.Notes;
            gameNight.MaxGuests = model.MaxGuests;
            gameNight.VenueId = model.VenueId;

            dbContext.GameNightGames.RemoveRange(gameNight.GameNightGames);

            List<int> validIds = await dbContext.Games
                .Where(g => model.SelectedGameIds.Contains(g.Id))
                .Select(g => g.Id)
                .ToListAsync();

            foreach (int gameId in validIds)
            {
                gameNight.GameNightGames.Add(new GameNightGame
                {
                    GameId = gameId
                });
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            GameNight? gameNight = await dbContext.GameNights.FindAsync(id);

            if (gameNight == null)
            {
                return false;
            }

            dbContext.GameNights.Remove(gameNight);
            await dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<IEnumerable<VenueOptionViewModel>> GetVenuesAsync()
        {
            return await dbContext.Venues
                .AsNoTracking()
                .OrderBy(v => v.Name)
                .Select(v => new VenueOptionViewModel
                {
                    Id = v.Id,
                    Name = v.Name
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<GameOptionViewModel>> GetGamesAsync()
        {
            return await dbContext.Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .Select(g => new GameOptionViewModel
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .ToListAsync();
        }

        public async Task<bool> JoinAsync(int gameNightId, string userId)
        {
            GameNight? gameNight = await dbContext.GameNights
                .FirstOrDefaultAsync(g => g.Id == gameNightId);

            if (gameNight == null || gameNight.Date <= DateTime.Now)
            {
                return false;
            }

            bool alreadyJoined = await dbContext.GameNightSignups
                .AnyAsync(s => s.GameNightId == gameNightId && s.UserId == userId);

            if (alreadyJoined)
            {
                return false;
            }

            int signupCount = await dbContext.GameNightSignups
                .CountAsync(s => s.GameNightId == gameNightId);

            if (signupCount >= gameNight.MaxGuests)
            {
                return false;
            }

            GameNightSignup signup = new GameNightSignup
            {
                GameNightId = gameNightId,
                UserId = userId,
                SignedUpOn = DateTime.UtcNow
            };

            await dbContext.GameNightSignups.AddAsync(signup);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LeaveAsync(int gameNightId, string userId)
        {
            GameNightSignup? signup = await dbContext.GameNightSignups
                .FirstOrDefaultAsync(s =>
                    s.GameNightId == gameNightId && s.UserId == userId);

            if (signup == null)
            {
                return false;
            }

            dbContext.GameNightSignups.Remove(signup);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsJoinedAsync(int gameNightId, string userId)
        {
            return await dbContext.GameNightSignups
                .AnyAsync(s => s.GameNightId == gameNightId && s.UserId == userId);
        }
        public async Task<bool> IsCreatorAsync(int gameNightId, string userId)
        {
            return await dbContext.GameNights
                .AnyAsync(g => g.Id == gameNightId && g.CreatorId == userId);
        }

    }

}

