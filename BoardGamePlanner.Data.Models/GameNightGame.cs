

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamePlanner.Data.Models
{
    [PrimaryKey(nameof(GameNightId), nameof(GameId))]
    public class GameNightGame
    {
        [ForeignKey(nameof(GameNight))]
        public int GameNightId { get; set; }
        public virtual GameNight GameNight { get; set; } = null!;
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; } = null!;
    }
}
