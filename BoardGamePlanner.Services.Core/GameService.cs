
using BoardGamePlanner.Data;
using BoardGamePlanner.Data.Models;
using BoardGamePlanner.Services.Core.Contracts;
using BoardGamePlanner.ViewModels.Games;
using Microsoft.EntityFrameworkCore;

namespace BoardGamePlanner.Services.Core
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext dbContext;

        public GameService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GameIndexViewModel>> GetAllAsync()
        {
            return await dbContext.Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .Select(g => new GameIndexViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    MinPlayers = g.MinPlayers,
                    MaxPlayers = g.MaxPlayers,
                    PlayTimeMinutes = g.PlayTimeMinutes,
                    Complexity = g.Complexity
                })
                .ToListAsync();
        }

        public async Task<GameDetailsViewModel?> GetDetailsAsync(int id)
        {
            return await dbContext.Games
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new GameDetailsViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    MinPlayers = g.MinPlayers,
                    MaxPlayers = g.MaxPlayers,
                    PlayTimeMinutes = g.PlayTimeMinutes,
                    Complexity = g.Complexity,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GameFormViewModel?> GetFormAsync(int id)
        {
            Game? game = await dbContext.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return null;
            }

            return new GameFormViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                MinPlayers = game.MinPlayers,
                MaxPlayers = game.MaxPlayers,
                PlayTimeMinutes = game.PlayTimeMinutes,
                Complexity = game.Complexity,
                ImageUrl = game.ImageUrl
            };
        }

        public async Task<int> CreateAsync(GameFormViewModel model)
        {
            Game game = new Game
            {
                Title = model.Title,
                Description = model.Description,
                MinPlayers = model.MinPlayers,
                MaxPlayers = model.MaxPlayers,
                PlayTimeMinutes = model.PlayTimeMinutes,
                Complexity = model.Complexity,
                ImageUrl = model.ImageUrl
            };

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();

            return game.Id;
        }

        public async Task<bool> UpdateAsync(GameFormViewModel model)
        {
            Game? game = await dbContext.Games
                .FindAsync(model.Id);

            if (game == null)
            {
                return false;
            }

            game.Title = model.Title;
            game.Description = model.Description;
            game.MinPlayers = model.MinPlayers;
            game.MaxPlayers = model.MaxPlayers;
            game.PlayTimeMinutes = model.PlayTimeMinutes;
            game.Complexity = model.Complexity;
            game.ImageUrl = model.ImageUrl;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Game? game = await dbContext.Games.FindAsync(id);

            if (game == null)
            {
                return false;
            }

            dbContext.Games.Remove(game);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
