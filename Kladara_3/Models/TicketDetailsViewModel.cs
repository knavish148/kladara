using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
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
