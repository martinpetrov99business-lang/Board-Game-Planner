using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BoardGamePlanner.Common.ValidationConstants.GameNight;
using Microsoft.AspNetCore.Identity;
namespace BoardGamePlanner.Data.Models
{
    public class GameNight
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        public DateTime Date { get; set; }
        [StringLength(NotesMaxLength)]
        public string? Notes { get; set; }
        [Range(MaxGuestsMin, MaxGuestsMax)]
        public int MaxGuests { get; set; }
        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }

        public virtual Venue Venue { get; set; } = null!;

        [Required]
        public string CreatorId { get; set; } = null!;

        [ForeignKey(nameof(CreatorId))]
        public virtual IdentityUser Creator { get; set; } = null!;

        public virtual ICollection<GameNightGame> GameNightGames { get; set; } = new List<GameNightGame>();

        public virtual ICollection<GameNightSignup> Signups { get; set; }= new List<GameNightSignup>();



    }
}
