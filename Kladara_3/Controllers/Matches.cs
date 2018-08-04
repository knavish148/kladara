using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kladara_3.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kladara_3.Controllers
{
    public class Matches : Controller
    {
        Match[] matches = new Match[]
        {
            new Match { Id = 1, Sport = "Football",   HomeTeam = "Barcelona",      AwayTeam = "Real Madrid",   HomeWins = 1.2, Tied = 1.5, AwayWins = 1.5 },
            new Match { Id = 2, Sport = "Football",   HomeTeam = "Bayern Munich",  AwayTeam = "Deportivo",     HomeWins = 1.1, Tied = 3.5, AwayWins = 5.5 },
            new Match { Id = 3, Sport = "Football",   HomeTeam = "Juventus",       AwayTeam = "Roma",          HomeWins = 1.5, Tied = 1.1, AwayWins = 1.8 },
            new Match { Id = 4, Sport = "Basketball", HomeTeam = "LA Lakers",      AwayTeam = "Chicago Bulls", HomeWins = 1.2, Tied = 1.5, AwayWins = 1.9 },
            new Match { Id = 5, Sport = "Basketball", HomeTeam = "Boston Celtics", AwayTeam = "Miami Heat",    HomeWins = 2.2, Tied = 1.3, AwayWins = 2.5 },
            new Match { Id = 6, Sport = "Handball",   HomeTeam = "RK Kastela",     AwayTeam = "Dugopolje",     HomeWins = 5.2, Tied = 1.8, AwayWins = 4.5 },
            new Match { Id = 7, Sport = "Volleyball", HomeTeam = "Split",          AwayTeam = "Omis",          HomeWins = 3.2, Tied = 3.5, AwayWins = 1.1 }
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
