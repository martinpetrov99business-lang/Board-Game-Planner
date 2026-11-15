
namespace BoardGamePlanner.ViewModels.GameNights
{
    public class GameNightDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime Date { get; set; }

        public string VenueName { get; set; } = null!;

        public int MaxGuests { get; set; }

        public string? Notes { get; set; }

        public string VenueAddress { get; set; } = null!;
    }
}
