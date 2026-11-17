
using BoardGamePlanner.ViewModels.Games;

namespace BoardGamePlanner.Services.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameIndexViewModel>> GetAllAsync(string? userId);
        Task<GameDetailsViewModel?> GetDetailsAsync(int id, string? userId);

        Task<GameFormViewModel?> GetFormAsync(int id);

        Task<int> CreateAsync(GameFormViewModel model, string userId);

        Task<bool> IsCreatorAsync(int gameId, string userId);

        Task<bool> IsInUseAsync(int gameId);

        Task<bool> UpdateAsync(GameFormViewModel model);

        Task<bool> DeleteAsync(int id);
    }
}
