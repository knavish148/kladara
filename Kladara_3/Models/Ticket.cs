using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
{
    public enum BetType
    {
        BetHome,
        BetTied,
        BetAway
    };

    public class Ticket
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Wager { get; set; }
        public int PossibleGain { get; set; }
        public List<Tuple<int, BetType>> Pairs { get; set; }
    }
}
