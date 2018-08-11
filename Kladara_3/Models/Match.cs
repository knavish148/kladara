namespace Kladara3.Models
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
    }
}
