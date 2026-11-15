
using Microsoft.AspNetCore.Identity;
namespace BoardGamePlanner.Data.Models
{
    public class GameNightSignup
    {
        public int GameNightId { get; set; }

        public virtual GameNight GameNight { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public DateTime SignedUpOn { get; set; }
    }
}
