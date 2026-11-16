
using BoardGamePlanner.ViewModels.Games;

namespace BoardGamePlanner.Services.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameIndexViewModel>> GetAllAsync();

        Task<GameDetailsViewModel?> GetDetailsAsync(int id);

        Task<GameFormViewModel?> GetFormAsync(int id);

        Task<int> CreateAsync(GameFormViewModel model);

        Task<bool> UpdateAsync(GameFormViewModel model);

        Task<bool> DeleteAsync(int id);
    }
}
