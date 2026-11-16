
using BoardGamePlanner.Common;
using System.ComponentModel.DataAnnotations;
using static BoardGamePlanner.Common.ValidationConstants.Game;

namespace BoardGamePlanner.ViewModels.Games
{
    public class GameFormViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
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

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (MinPlayers > MaxPlayers)
            {
                yield return new ValidationResult(
                    "Minimum players cannot be greater than maximum players.",
                    new[] { nameof(MinPlayers) });
            }
        }
    }
}
