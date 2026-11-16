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

        public async Task<IEnumerable<GameNightIndexViewModel>> GetAllAsync()
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
                    MaxGuests = g.MaxGuests
                })
                .ToListAsync();
        }

        public async Task<GameNightDetailsViewModel?> GetDetailsAsync(int id)
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
                    Notes = g.Notes
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GameNightFormViewModel> GetCreateModelAsync()
        {
            return new GameNightFormViewModel
            {
                Date = DateTime.Today.AddDays(7).AddHours(19),
                Venues = await GetVenuesAsync()
            };
        }

        public async Task<int> CreateAsync(GameNightFormViewModel model)
        {
            GameNight gameNight = new GameNight
            {
                Title = model.Title,
                Date = model.Date,
                Notes = model.Notes,
                MaxGuests = model.MaxGuests,
                VenueId = model.VenueId
            };

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
                Venues = await GetVenuesAsync()
            };
        }

        public async Task<bool> UpdateAsync(GameNightFormViewModel model)
        {
            GameNight? gameNight = await dbContext.GameNights
                .FindAsync(model.Id);

            if (gameNight == null)
            {
                return false;
            }

            gameNight.Title = model.Title;
            gameNight.Date = model.Date;
            gameNight.Notes = model.Notes;
            gameNight.MaxGuests = model.MaxGuests;
            gameNight.VenueId = model.VenueId;

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
    }

}

