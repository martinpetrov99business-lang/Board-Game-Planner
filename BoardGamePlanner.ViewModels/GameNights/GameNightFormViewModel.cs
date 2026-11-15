using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoardGamePlanner.Common;
using static BoardGamePlanner.Common.ValidationConstants.GameNight;

namespace BoardGamePlanner.ViewModels.GameNights
{
    public class GameNightFormViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;
        
        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Range(MaxGuestsMin, MaxGuestsMax)]
        public int MaxGuests { get; set; }

        [StringLength(NotesMaxLength)]
        public string? Notes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a venue.")]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        public IEnumerable<VenueOptionViewModel> Venues { get; set; } = new List<VenueOptionViewModel>();
    }
}
