using System;
using System.Collections.Generic;

namespace Kladara3.Models
{
    public class TicketDetailsViewModel
    {
        public int Id { get; set; }
        public int Wager { get; set; }
        public int Bonus { get; set; }
        public double PossibleGain { get; set; }
        public DateTime Date { get; set; }
        public List<PairDetails> PairDetails { get; set; }
    }

    public class PairDetails
    {
        public Pair Pair { get; set; }
        public Match Match { get; set; }
    }
}
