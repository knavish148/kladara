using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kladara_3.Models;

namespace Kladara_3.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Kladara_3Context _context;
        private static List<Pair> userPairs = new List<Pair>();

        // ctor
        public TicketsController(Kladara_3Context context)
        {
            _context = context;
        }

        // static field getter/setter
        public static NewTicketData NewTicketData { get; set; }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            NewTicketData = new NewTicketData
            {
                Wager = 0,
                Wallet = WalletTransactionsController.GetWalletState(_context),
                PossibleGain = 0.00,
                Bonus = 0
            };
            userPairs = new List<Pair>();
            return View(await _context.Ticket.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketDetailsViewModel ticketMatches = new TicketDetailsViewModel
            {
                Wager = ticket.Wager,
                PossibleGain = ticket.PossibleGain
            };

            List<PairDetails> pairDetails = new List<PairDetails>();
            List<int> pairIds = new List<int>();
            foreach (var pairId in ticket.Pairs.Split(','))
                pairIds.Add(Int32.Parse(pairId));

            foreach (var pairId in pairIds)
            {
                Pair pair = _context.Pair
                .SingleOrDefault(m => m.Id == pairId);

                List<Match> matches = _context.Match.ToList();

                Match match = _context.Match.SingleOrDefault(m => m.Id == pair.MatchId);

                pairDetails.Add(new PairDetails
                {
                    Pair = pair,
                    Match = match
                });
            }

            ticketMatches.PairDetails = pairDetails;

            return View(ticketMatches);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tickets/UpdatePairs/<match_id>_1
        // POST: Tickets/UpdatePairs/<match_id>_X
        // POST: Tickets/UpdatePairs/<match_id>_2
        public string UpdatePairs(string matchId_bet)
        {
            // Update list of unsubmitted tickets
            if (matchId_bet == null)
            {
                UpdateNewTicketData();
                return "fail";
            }

            string[] data = matchId_bet.Split('_');
            // parse match id
            if (!Int32.TryParse(data[0], out int matchId))
            {
                UpdateNewTicketData();
                return "fail";
            }


            Pair p = userPairs.Find(f => f.MatchId == matchId);

            // User deselects the pair he selected previously
            if (p != null && p.Bet == Pair.GetBetType(data[1]))
            {
                userPairs.Remove(p);
            }
            // User bets on same match but changes the bet (1/X/2)
            else if (p != null)
            {
                userPairs.Remove(p);
                // Create new Pair instance and add it to the list   
                Pair pNew = _context.Pair.SingleOrDefault(f => f.MatchId == matchId && f.Bet == Pair.GetBetType(data[1]));
                userPairs.Add(pNew);
            }
            // User bets on a new match
            else
            {
                // Obtain Pair instance from DB and add it to the list of user selected pairs 
                Pair pNew = _context.Pair.SingleOrDefault(f => f.MatchId == matchId && f.Bet == Pair.GetBetType(data[1]));
                userPairs.Add(pNew);
            }

            // Inform client of successful post action
            UpdateNewTicketData();
            return "success";
        }

        // POST: Tickets/WagerUpdate/<new_wager_value>
        public string WagerUpdate(int value)
        {
            NewTicketData.Wager = value;
            UpdateNewTicketData();
            return "success";
        }

        // POST: Tickets/Submit
        public string Submit()
        {

            Ticket ticket = new Ticket
            {
                Wager = NewTicketData.Wager,
                PossibleGain = (int)NewTicketData.PossibleGain,
                Pairs = Pair.StringifyPairs(userPairs)
            };

            _context.Add(ticket);
            _context.SaveChanges();

            UpdateWalletState();
            System.Threading.Thread.Sleep(1000);
            ResetNewTicketData();
            userPairs = new List<Pair>();

            return "Matches";
        }

        // GET Tickets/RefreshNewTicketData
        public IActionResult RefreshNewTicketDataView()
        {
            // Update client-side view
            return PartialView("_NewTicketSubmit", NewTicketData);
        }


        // --------------- HELPERS ---------------

        public double GetBetAmount(Match match, BetType t)
        {
            if (t == BetType.BetHome)
                return match.HomeWins;
            else if (t == BetType.BetTied)
                return match.Tied;
            else
                return match.AwayWins;
        }

        public async void UpdateWalletState()
        {
            double walletState = WalletTransactionsController.GetWalletState(_context);
            WalletTransaction transaction = new WalletTransaction
            {
                WalletBefore = walletState,
                WalletAfter = walletState - NewTicketData.Wager,
                TransactionDate = DateTime.Now
            };
            await new WalletTransactionsController(_context).Create(transaction);
            return;
        }

        // Calculate possible gain based on wager entered and selected pairs
        public double CalculatePossibleGain(int wager, int bonus)
        {
            double gain = (double)wager;

            if (!userPairs.Any())
                return 0;

            foreach (var pair in userPairs.ToList())
            {
                var match = _context.Match
                .SingleOrDefault(m => m.Id == pair.MatchId);

                gain *= GetBetAmount(match, pair.Bet);
            }

            return gain + bonus;
        }

        // Add bonuses on possible gain sum to user:
        // - Add 5 if user on three pairs from same sport
        // - Add 10 if user bet on at least one pair from all sports
        public int CalculateBonus()
        {
            List<Match> userMatches = new List<Match>();
            List<Match> allMatches = _context.Match.ToList();
            int bonus = 0;

            foreach (var pair in userPairs.ToList())
            {
                var match = _context.Match
                .SingleOrDefault(m => m.Id == pair.MatchId);

                userMatches.Add(match);
            }

            var countSameSport = (from m in userMatches
                                  group m by m.Sport into g
                                  let count = g.Count()
                                  orderby count descending
                                  select count).First();

            if (countSameSport >= 3) 
                bonus += 5;

            var countAllSports = (from m in allMatches
                                  group m by m.Sport into g
                                  select g).Count();

            var countUserSports = (from m in userMatches
                                   group m by m.Sport into g
                                   select g).Count();

            if (countAllSports == countUserSports)
                bonus += 10;

            return bonus; 
        }

        public void UpdateNewTicketData()
        {
            NewTicketData.Wallet = WalletTransactionsController.GetWalletState(_context);
            NewTicketData.Bonus = CalculateBonus();
            NewTicketData.PossibleGain = CalculatePossibleGain(NewTicketData.Wager, NewTicketData.Bonus);
        }

        public void ResetNewTicketData()
        {
            NewTicketData.Wallet = WalletTransactionsController.GetWalletState(_context);
            NewTicketData.Bonus = 0;
            NewTicketData.PossibleGain = 0;
        }
    }
}
