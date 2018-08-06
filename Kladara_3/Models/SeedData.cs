using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Kladara_3.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Kladara_3Context(
                serviceProvider.GetRequiredService<DbContextOptions<Kladara_3Context>>()))
            {
                // Look for any movies.
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
                context.SaveChanges();
            }
        }
    }
}
