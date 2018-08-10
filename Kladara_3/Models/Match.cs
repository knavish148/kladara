using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public double HomeWins { get; set; }
        public double Tied { get; set; }
        public double AwayWins { get; set; }

        public Match()
        {
        }

        public Match(Match m)
        {
            Id = m.Id;
            Sport = m.Sport;
            HomeTeam = m.HomeTeam;
            AwayTeam = m.AwayTeam;
            HomeWins = m.HomeWins;
            Tied = m.Tied;
            AwayWins = m.AwayWins;
        }

        public static Match GetMatch(List<Match> matches, int id)
        {
            Match m = matches.Find(x => x.Id == id);
            return new Match(m);
        }
    }
}
