

namespace BoardGamePlanner.Data.Models
{
    public class GameNightGame
    {
        public int GameNightId { get; set; }
        public virtual GameNight GameNight { get; set; } = null!;
        public int GameId { get; set; }
        public virtual Game Game { get; set; } = null!;
    }
}
