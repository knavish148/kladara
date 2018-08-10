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

    public class Pair
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public BetType Bet { get; set; }

        // In order to save DB space stringify a list of pairs into a 
        // string that consists of Pair Id's separated by commas
        public static string StringifyPairs(List<Pair> pairs)
        {
            string result = "";

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
            if (betStr == "1")
                return BetType.BetHome;
            else if (betStr == "X")
                return BetType.BetTied;
            else
                return BetType.BetAway;
        }
    }
}
