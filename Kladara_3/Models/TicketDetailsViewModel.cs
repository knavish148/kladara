using System.Collections.Generic;

namespace Kladara3.Models
{
    public class TicketDetailsViewModel
    {
        public int Id { get; set; }
        public int Wager { get; set; }
        public int PossibleGain { get; set; }
        public List<PairDetails> PairDetails { get; set; }
    }

    public class PairDetails
    {
        public Pair Pair { get; set; }
        public Match Match { get; set; }
    }
}
