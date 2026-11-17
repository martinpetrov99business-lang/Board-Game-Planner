
using BoardGamePlanner.Common;


namespace BoardGamePlanner.ViewModels.Games
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int PlayTimeMinutes { get; set; }

        public Complexity Complexity { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsCreator { get; set; }
    }
}
