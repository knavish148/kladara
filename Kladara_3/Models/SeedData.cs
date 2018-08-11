using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Kladara3.Data;

namespace Kladara3.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Kladara3Context(
                serviceProvider.GetRequiredService<DbContextOptions<Kladara3Context>>()))
            {
                SeedMatches(context);
                SeedPairs(context);
                SeedWallet(context);

                context.SaveChanges();
            }
        }

        private static void SeedMatches(Kladara3Context context)
        {
            // Look for any matches in DB
            if (context.Match.Any())
            {
                return;   // DB has been seeded
            }

            context.Match.AddRange(
                new Match
                {
                    Sport = "Football",
                    HomeTeam = "Barcelona",
                    AwayTeam = "Real Madrid",
                    HomeWins = 1.8,
                    Tied = 1.6,
                    AwayWins = 1.2
                },

                new Match
                {
                    Sport = "Football",
                    HomeTeam = "Bayern Munich",
                    AwayTeam = "Borrusia",
                    HomeWins = 1.25,
                    Tied = 1.35,
                    AwayWins = 1.55
                },

                new Match
                {
                    Sport = "Football",
                    HomeTeam = "Juventus",
                    AwayTeam = "Roma",
                    HomeWins = 1.25,
                    Tied = 1.45,
                    AwayWins = 1.55
                },

                new Match
                {
                    Sport = "Basketball",
                    HomeTeam = "LA Lakers",
                    AwayTeam = "Chicago Bulls",
                    HomeWins = 1.15,
                    Tied = 1.09,
                    AwayWins = 1.58
                },

                new Match
                {
                    Sport = "Basketball",
                    HomeTeam = "Boston Celtics",
                    AwayTeam = "Miami Heat",
                    HomeWins = 3.99,
                    Tied = 14.5,
                    AwayWins = 1.18
                },

                new Match
                {
                    Sport = "Handball",
                    HomeTeam = "RK Kastela",
                    AwayTeam = "RK Split",
                    HomeWins = 1.35,
                    Tied = 1.75,
                    AwayWins = 1.45
                }
            );
        }

        private static void SeedPairs(Kladara3Context context)
        {
            // Look for any pairs in DB
            if (context.Pair.Any())
            {
                return;   // DB has been seeded
            }

            foreach (var match in context.Match.ToList())
            {
                context.Pair.AddRange(
                    new Pair
                    {
                        MatchId = match.Id,
                        Bet = BetType.BetHome
                    },
                    new Pair
                    {
                        MatchId = match.Id,
                        Bet = BetType.BetTied
                    },
                    new Pair
                    {
                        MatchId = match.Id,
                        Bet = BetType.BetAway
                    }
                );
            }

        }

        private static void SeedWallet(Kladara3Context context)
        {
            // Look for any wallet data in DB
            if (context.WalletTransaction.Any())
            {
                return;   // DB has been seeded
            }

            context.WalletTransaction.Add(
                    new WalletTransaction
                    {
                        WalletBefore = 5000.00,
                        WalletAfter = 5000.00,
                        TransactionDate = DateTime.Now
                    }
                );
        }
    }
}
