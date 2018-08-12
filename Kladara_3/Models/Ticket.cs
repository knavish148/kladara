using System;

namespace Kladara3.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Wager { get; set; }
        public int Bonus { get; set; }
        public double PossibleGain { get; set; }
        public string Pairs { get; set; }
        public DateTime Date { get; set; }
    }
}
