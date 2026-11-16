using System.ComponentModel.DataAnnotations;
using static BoardGamePlanner.Common.ValidationConstants.GameNight;
using BoardGamePlanner.ViewModels.Validation;

namespace BoardGamePlanner.ViewModels.GameNights
{
    public class GameNightFormViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;
        [FutureDate(ErrorMessage = "The game night date must be in the future.")]
        [Required,DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Range(MaxGuestsMin, MaxGuestsMax)]
        [Display(Name = "Max guests")]
        public int MaxGuests { get; set; }

        [StringLength(NotesMaxLength)]
        public string? Notes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a venue.")]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        public IEnumerable<VenueOptionViewModel> Venues { get; set; } = new List<VenueOptionViewModel>();
    }
}
