using System;
using System.Collections.Generic;
using System.Linq;

namespace Kladara3.Models
{
    public enum BetType
    {
        BetHome,
        BetTied,
        BetAway
    };

    public class Pair
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public BetType Bet { get; set; }

        // In order to save DB space stringify a list of pairs into a 
        // string that consists of Pair Id's separated by commas
        public static string StringifyPairs(List<Pair> pairs)
        {
            var result = "";

            foreach (var pair in pairs)
            {
                result += pair.Id.ToString();

                if (pair != pairs.Last())
                {
                    result += ',';
                }
            }

            return result;
        }

        public static BetType GetBetType(string betStr)
        {
            switch (betStr)
            {
                case "1":
                    return BetType.BetHome;
                case "X":
                    return BetType.BetTied;
                case "2":
                    return BetType.BetAway;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
