using System.Collections.Generic;

namespace Kladara3.Models
{
    public class MatchesNewTicketViewModel
    {
        public List<Match> Matches { get; set; }
        public NewTicketData NewTicketData { get; set; }
    }

    public class NewTicketData
    {
        public int Wager { get; set; }
        public double Wallet { get; set; }
        public double PossibleGain { get; set; }
        public int Bonus { get; set; }
    }
}
