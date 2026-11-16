using BoardGamePlanner.ViewModels.GameNights;

namespace BoardGamePlanner.Services.Core.Contracts
{
    public interface IGameNightService
    {
        Task<bool> FitsVenueAsync(int venueId, int maxGuests);
        Task<IEnumerable<GameNightIndexViewModel>> GetAllAsync();

        Task<GameNightDetailsViewModel?> GetDetailsAsync(int id);

        Task<GameNightFormViewModel> GetCreateModelAsync();

        Task<int> CreateAsync(GameNightFormViewModel model);

        Task<GameNightFormViewModel?> GetEditModelAsync(int id);

        Task<bool> UpdateAsync(GameNightFormViewModel model);

        Task<bool> DeleteAsync(int id);
    }
}