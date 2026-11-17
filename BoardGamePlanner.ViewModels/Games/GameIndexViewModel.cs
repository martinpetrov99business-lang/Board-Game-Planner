
using BoardGamePlanner.Common;

namespace BoardGamePlanner.ViewModels.Games
{
    public class GameIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public int PlayTimeMinutes { get; set; }

        public bool IsCreator { get; set; }

        public Complexity Complexity { get; set; }

        
    }
}
