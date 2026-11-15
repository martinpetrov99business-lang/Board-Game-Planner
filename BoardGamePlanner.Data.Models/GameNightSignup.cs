
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace BoardGamePlanner.Data.Models
{
    [PrimaryKey(nameof(GameNightId), nameof(UserId))]
public class GameNightSignup
{
    [ForeignKey(nameof(GameNight))]
    public int GameNightId { get; set; }
    public virtual GameNight GameNight { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public virtual IdentityUser User { get; set; } = null!;

    public DateTime SignedUpOn { get; set; }
}
}
