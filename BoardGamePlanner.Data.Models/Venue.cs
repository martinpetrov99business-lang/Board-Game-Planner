using BoardGamePlanner.Common;
using System.ComponentModel.DataAnnotations;
using static BoardGamePlanner.Common.ValidationConstants.Venue;

namespace BoardGamePlanner.Data.Models
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(NameMaxLength,MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
       
        [Required,StringLength(AddressMaxLength,MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;
        [Range(CapacityMin,CapacityMax)]
        public int Capacity { get; set; }

        public virtual ICollection<GameNight> GameNights { get; set; } = new HashSet<GameNight>();
    }
}
