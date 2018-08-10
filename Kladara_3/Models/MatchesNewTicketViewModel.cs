using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
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
