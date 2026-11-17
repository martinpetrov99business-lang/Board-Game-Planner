using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using BoardGamePlanner.Common;
using System.ComponentModel.DataAnnotations;
using static BoardGamePlanner.Common.ValidationConstants.Game;

namespace BoardGamePlanner.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;
        [StringLength(DescriptionMaxLength)]
        public string? Description { get; set; }
        [Range(PlayersMin, PlayersMax)]
        public int MinPlayers { get; set; }
        [Range(PlayersMin, PlayersMax)]
        public int MaxPlayers { get; set; }

        [Range(PlayTimeMin, PlayTimeMax)]
        public int PlayTimeMinutes { get; set; }

        public Complexity Complexity { get; set; }
        [StringLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
        public string? CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual IdentityUser? Creator { get; set; }
        public virtual ICollection<GameNightGame> GameNightGames { get; set; } = new List<GameNightGame>();



    }
}
