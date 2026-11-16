using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamePlanner.ViewModels.GameNights
{
    public class GameNightIndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime Date { get; set; }

        public string VenueName { get; set; } = null!;

        public int MaxGuests { get; set; }

        public int SignedUpCount { get; set; }

        public bool IsJoined { get; set; }
    }
}
