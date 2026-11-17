
using BoardGamePlanner.ViewModels.GameNights;

namespace BoardGamePlanner.Services.Core.Contracts
{
    public interface IGameNightService
    {
        Task<IEnumerable<GameNightIndexViewModel>> GetAllAsync(string? userId);

        Task<GameNightDetailsViewModel?> GetDetailsAsync(int id, string? userId);

        Task<GameNightFormViewModel> GetCreateModelAsync();

        Task<int> CreateAsync(GameNightFormViewModel model, string userId);

        Task<bool> IsCreatorAsync(int gameNightId, string userId);

        Task<GameNightFormViewModel?> GetEditModelAsync(int id);

        Task<bool> UpdateAsync(GameNightFormViewModel model);

        Task<bool> DeleteAsync(int id);

        Task<bool> FitsVenueAsync(int venueId, int maxGuests);

        Task<bool> JoinAsync(int gameNightId, string userId);

        Task<bool> LeaveAsync(int gameNightId, string userId);

        Task<int> GetSignupCountAsync(int gameNightId);

        Task<IEnumerable<string>> GetGamesNeedingMorePlayersAsync( IEnumerable<int> gameIds, int maxGuests);
    }
}
